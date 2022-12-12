using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")]
    public GameObject player;
    public static Difficulties currentDifficulty;
    public Difficulties temporal;
    [SerializeField] private Transform controls;
    private static bool firstTimeControl;

    [Header("Panel to activate")]
    [SerializeField] private GameObject basePanel;
    [SerializeField] private Transform pausePanel;

    public bool isPaused { get; set; }
    private bool panelActive { get; set; }
    public bool occupied { get; set; }

    [Header("Pop Ups in panel")]
    [SerializeField] private Transform defeatPanel;
    [SerializeField] private Transform victoryPanel;

    [Header("Scene and video select")]
    [SerializeField] private bool isTheLastLevel;
    [SerializeField] private string sceneToChange;
    [SerializeField] private int video;

    [Header("TweenScript")]
    [SerializeField] private TweenedPopups tweenAnimations;

    #region EVENTS
    public System.Action EnemiesPositionated;
    public System.Action enemyKilled;
    public System.Action playerKilled;
    public System.Action levelCompleted;
    #endregion

    #region ENEMIES
    public bool enemiesOn { get; set; }
    public int amountKilled { get; set; }
    public int amountAlive => totalInvaders - amountKilled;
    public int totalInvaders
    {
        get
        {
            if (FindObjectOfType<EnemiesGridController>() == null) return 0;
            else return FindObjectOfType<EnemiesGridController>().rows * FindObjectOfType<EnemiesGridController>().columns;
        }
    }
    public float percentKilled => (float)amountKilled / totalInvaders;
    #endregion

    #region SCORE
    public int currentLevelScore { get; set; }
    public int[] individualShipScore { get; set; }
    public static int accumulatedScore { get; set; }
    public Enemy currentEnemy { get; set; }
    #endregion

    private void Awake()
    {
        //BORRAR DESPUES 
        if (currentDifficulty == null) currentDifficulty = temporal;

        Instance = this;

        individualShipScore = new int[5];

        #region SUBSCRIBERS
        enemyKilled += OnInvaderKilled;
        playerKilled += OnPlayerKilled;
        EnemiesPositionated += OnEnemiesPositionated;
        levelCompleted += OnLevelCompleted;
        #endregion
    }

    private void Start()
    {
        //visualTotalScore = DataManager.temporalTotal;
        TryShowControls();
    }

    private void OnDisable()
    {
        #region UNSUBSCRIBE
        enemyKilled -= OnInvaderKilled;
        playerKilled -= OnPlayerKilled;
        EnemiesPositionated -= OnEnemiesPositionated;
        levelCompleted -= OnLevelCompleted;
        #endregion
    }
    private void OnInvaderKilled()
    {
        amountKilled++;

        switch (currentEnemy)
        {
            case Enemy.etherium: UpdateScore(1, 0); break;
            case Enemy.tether: UpdateScore(2, 1); break;
            case Enemy.libra: UpdateScore(3, 2); break;
            case Enemy.doge: UpdateScore(4, 3); break;
            case Enemy.fed: UpdateScore(10, 4); break;
        }
    }

    private void UpdateScore(int score, int individualShipIndex)
    {
        currentLevelScore += score;
        //individualShipScore[individualShipIndex]++;
        SaveSystem.data.individualScores[individualShipIndex]++;
    }

    private void OnPlayerKilled()
    {
        if (occupied) return;
        if (panelActive) return;
        if(isPaused) return;

        occupied = true;
        panelActive = true;
        if (SaveSystem.data.hasDiedFirstTime == false)
            SaveSystem.data.hasDiedFirstTime = true;
        SaveSystem.Save();
        AudioManager.Instance.Play("Player@Explo", AudioManager.Instance.sounds);
        player.GetComponent<Animator>().Play("Bitcoin@Explosion");
        PauseState(defeatPanel);
    } 

    public void ClrearOcupied()
    {
        if(occupied) occupied = false;

    }

    private void TryShowControls()
    {
        if (!firstTimeControl) StartCoroutine(Controls());
    }
    private IEnumerator Controls()
    {
        yield return new WaitForSeconds(1);
        firstTimeControl = true;
        controls.gameObject.SetActive(true);
        tweenAnimations.AppearPopUps(controls);
        yield return new WaitForSeconds(3.5f);
        tweenAnimations.DisappearPopUps(controls);
        tweenAnimations.CallDeactivatePanel(controls.gameObject);
    }

    private void OnEnemiesPositionated() => enemiesOn = true;
    private void OnLevelCompleted()
    {
        if (occupied) return;
        if (panelActive) return;

        GetComponent<AnalyticsEventSender>().SendEvent("WonLevel");

        occupied = true;

        panelActive = true;

        SaveSystem.data.totalScore += currentLevelScore;
        accumulatedScore += currentLevelScore;
        currentLevelScore = 0;
        SaveSystem.Save();

        if (isTheLastLevel)
        {
            PauseState(victoryPanel);
            SaveSystem.data.gameEnded = true;
            SaveSystem.Save();
        }
        else
        {
            //PARA GUARDAR NAVES DESTRUIDAS DESPUES
            /*for(int i = 0; i < SaveSystem.data.individualScores.Length; i++)
                SaveSystem.data.individualScores[i] += individualShipScore[i];*/
            ChangeScene cs = FindObjectOfType<ChangeScene>();
            cs.SelectVideo(video);
            cs.Change(sceneToChange);
        }
    }

    public void PlayAgain() => StartCoroutine(ResetScore());
    private IEnumerator ResetScore()
    {
        yield return new WaitForSeconds(.8f);
        accumulatedScore = 0;
    }

    private void ActivatePanel(Transform specificPanel)
    {
        basePanel.SetActive(true);
        specificPanel.gameObject.SetActive(true);
        tweenAnimations.AppearPopUps(specificPanel);
    }

    private bool pause;
    public void PauseState(Transform panel)
    {
        pause = !pause;
        //isPaused = !isPaused;

        if (pause)
        {
            isPaused = true;
            panelActive = true;
            ActivatePanel(panel);           
        }        
        else
        {
            panelActive = false;
            tweenAnimations.DisappearPopUps(panel);
            tweenAnimations.CallDeactivatePanel(basePanel); 
            StartCoroutine(pauseDelay(panel));
        }
    }

    private IEnumerator pauseDelay(Transform panel)
    {
        yield return new WaitUntil(() => basePanel.activeInHierarchy == false && panel.localScale == Vector3.zero);
        isPaused = false;
    }

    private void Update() => PauseWithKeys();

    private void PauseWithKeys()
    {
        Debug.Log(occupied);

        //if (Input.GetKeyDown(KeyCode.T))
        //    playerKilled?.Invoke();
        //if (Input.GetKeyDown(KeyCode.R))
        //    levelCompleted?.Invoke();

        if (occupied) return;

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !panelActive)
            StartCoroutine(PausePanelDelay(Vector2.zero));
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && panelActive)
            StartCoroutine(PausePanelDelay(Vector2.one));
    }

    private IEnumerator PausePanelDelay(Vector2 vector)
    {
        Vector2 scale = pausePanel.localScale;
        yield return new WaitUntil(() => scale == vector);
        PauseState(pausePanel);
    }
}
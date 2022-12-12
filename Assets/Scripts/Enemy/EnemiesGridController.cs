using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable] public struct RowOrder
{
    public GameObject[] enemies;
}
public class EnemiesGridController : MonoBehaviour
{
    [SerializeField] private GameObject[] shields;
    [SerializeField] private GameObject enemiesParent;
    private int currentLevel;
    private int totalLevels;
    private int levelIndex;
    private RowOrder[] phases;

    public Transform[] enemySpawnPositions;

    [Header("Spacing")]
    [Tooltip("Space between x grid elements")] [SerializeField] [Range(0, 10)] private float xSpacing;
    [Tooltip("Space between y grid elements")] [SerializeField] [Range(0, 10)] private float ySpacing;
    
    [HideInInspector] public int columns;
    [HideInInspector] public int rows;

    [SerializeField] private float playerKillPos;

    private Vector2 initialPos;

    //Bools to invoke one time
    private bool canBuild = true;
    private bool invokeKillOnce = true;
    private bool canCheckIfPositionated; // called true when grid builded

    void Start() 
    {
        //Asign level itself
        levelIndex = SceneManager.GetActiveScene().buildIndex - 1; // Current buildindex without Splash and Menu

        //Asign enemies depending on level
        switch(levelIndex)
        {
            case 0: 
                phases = new RowOrder[GameManager.currentDifficulty.level0.Length]; 
                phases = GameManager.currentDifficulty.level0; 
                break;
            case 1:
                phases = new RowOrder[GameManager.currentDifficulty.level1.Length];
                phases = GameManager.currentDifficulty.level1; 
                break;
            case 2:
                phases = new RowOrder[GameManager.currentDifficulty.level2.Length];
                phases = GameManager.currentDifficulty.level2; 
                break;
            case 3:
                phases = new RowOrder[GameManager.currentDifficulty.level3.Length];
                phases = GameManager.currentDifficulty.level3; 
                break;
        }

        //Asign rows & columns
        rows = phases[0].enemies.Length;
        columns = GameManager.currentDifficulty.columns;

        initialPos.x = 0;
        totalLevels = phases.Length - 1;
        BuildGrid();
    }

    private void Update() 
    {
        ChangePhaseOrLevel();

        if (canBuild) BuildGrid();

        CheckIfEnemiesPositionated();

        if(GameManager.Instance.player.activeInHierarchy) Defeat();
    }
    
    private void ChangePhaseOrLevel()
    {
        if (transform.childCount <= 0)
        {
            canBuild = true;
            GameManager.Instance.amountKilled = 0;

            if (currentLevel >= totalLevels)
            {
                canBuild = false;
                GameManager.Instance.levelCompleted?.Invoke();        
            }
            else
            {
                currentLevel++;
                rows = phases[currentLevel].enemies.Length;
            }
        }
    }

    void CheckIfEnemiesPositionated()
    {
        if (canCheckIfPositionated)
            foreach (Transform invaders in transform)
                if (invaders.GetComponentInChildren<MoveToActualPos>().enabled == false)
                {
                    canCheckIfPositionated = false;
                    GameManager.Instance.EnemiesPositionated?.Invoke();
                }
    }

    void Defeat()
    {
        if (GameManager.Instance.enemiesOn && invokeKillOnce)
        { 
            int count = 0;
            for (int i = 0; i < shields.Length; i++)
                if (shields[i].transform.childCount == 0)
                    count++;
            if (count >= 4 && transform.position.y <= playerKillPos)
            {
                invokeKillOnce = false;
                GameManager.Instance.playerKilled?.Invoke();
            }
        }
    }
    private Transform[] positionsArray;
     void BuildGrid()
    {
        canBuild = false;

        switch (rows)
        {
            case 1: initialPos.y = 3.5f; break;
            case 2: initialPos.y = 3.1f; break;
            case 3: initialPos.y = 2.7f; break;
            case 4: initialPos.y = 2.3f; break;
            case 5: initialPos.y = 1.9f; break;
            case 6: initialPos.y = 1.5f; break;
        }
        transform.position = initialPos;

        canCheckIfPositionated = true;
        GameManager.Instance.enemiesOn = false;

        for (int y = 0; y < rows; y++)
        {
            float width = xSpacing * (columns - 1);
            float heigth = ySpacing * (rows - 1);

            Vector2 centering = new Vector2(-width / 2, -heigth / 2);
            Vector3 rowPos = new Vector3(centering.x, centering.y + (y * ySpacing), 0);

            for (int x = 0; x < columns; x++)
            {
                GameObject invader = Instantiate(phases[currentLevel].enemies[y], enemiesParent.transform);

                Vector3 pos = rowPos;
                pos.x += x * xSpacing;
                invader.transform.localPosition = pos;
            }
        }
    }

    public void ResetPosition()
    {
        gameObject.transform.position = new Vector2(0, 0);
        transform.position = initialPos;

    }
}

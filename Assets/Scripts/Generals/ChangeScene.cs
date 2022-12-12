using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private float duration;
    private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image bar;
    [SerializeField] private GameObject[] coins = new GameObject[4];
    private bool stopLoop;
    public AsyncOperation operation;

    void Awake() => canvasGroup = GetComponent<CanvasGroup>();

    void Start()
    {
        FadeCanvasGroup(canvasGroup, 1, 0, duration);
        StartCoroutine(deactiveTMPro());
    }

    private IEnumerator deactiveTMPro()
    {
        yield return new WaitUntil(() => canvasGroup.alpha == 0);
        text.enabled = false;
    }

    public void Change(string sceneName) => StartCoroutine(TransitionLoadScene(sceneName));

    public void Reload() => Change(SceneManager.GetActiveScene().name);

    private IEnumerator TransitionLoadScene(string sceneName)
    {
        GetComponent<Image>().raycastTarget = true;
        FadeCanvasGroup(canvasGroup, 0, 1, duration);
        yield return new WaitForSeconds(duration);
        //SceneManager.LoadScene(sceneName);
        

        text.enabled = true;

        yield return new WaitUntil(() => GetComponent<CanvasGroup>().alpha == 1);
        FadeCanvasGroup(transform.GetChild(0).gameObject.GetComponent<CanvasGroup>(), 0, 1, .3f);
        yield return new WaitUntil(() => transform.GetChild(0).gameObject.GetComponent<CanvasGroup>().alpha == 1);

        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        StartCoroutine(LoadingText());

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            bar.fillAmount = progress;
            StartCoroutine(LoadingIcons());
            //Debug.Log(operation.progress);
            if (operation.progress >= .9f)
                operation.allowSceneActivation = true;
            yield return null;
        }
    }

    private IEnumerator LoadingIcons()
    {
        if (!stopLoop)
            for (int i = 0; i < coins.Length; i++)
            {
                stopLoop = true;
                coins[i].LeanMoveLocalY(60, .5f).setEaseInBack().setEaseOutBounce();
                yield return new WaitForSeconds(.8f);
                coins[i].LeanMoveLocalY(20, .5f).setEaseOutBounce();
                yield return new WaitForSeconds(.8f);
                stopLoop = false;
            }
    }

    private IEnumerator LoadingText()
    {
        while (!operation.isDone)
        {
            if (SaveSystem.data.language == 0)
            {
                text.text = "LOADING";
                yield return new WaitForSeconds(1f);
                text.text = "LOADING.";
                yield return new WaitForSeconds(1f);
                text.text = "LOADING..";
                yield return new WaitForSeconds(1f);
                text.text = "LOADING...";
                yield return new WaitForSeconds(1f);
            }
            else
            {
                text.text = "CARGANDO";
                yield return new WaitForSeconds(1f);
                text.text = "CARGANDO.";
                yield return new WaitForSeconds(1f);
                text.text = "CARGANDO..";
                yield return new WaitForSeconds(1f);
                text.text = "CARGANDO...";
                yield return new WaitForSeconds(1f);
            }
        }
    }

    public void FadeCanvasGroup(CanvasGroup canvasGroup, float initialAlpha, float finalAlpha, float time)
    {
        canvasGroup.alpha = initialAlpha;
        LeanTween.alphaCanvas(canvasGroup, finalAlpha, time * Time.deltaTime);
    }
    public void SelectVideo(int clipNumber) => Videos.clipNmbr = clipNumber;
    public void SelectDifficulty(Difficulties difficulty) => GameManager.currentDifficulty = difficulty;
    public void Quit() => Application.Quit();
}
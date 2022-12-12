using UnityEngine;
using UnityEngine.Localization.Settings;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private ChangeScene cs;
    [SerializeField] private MenuAnimations menuAnims;
    public static bool isFirstTime = true;

    private void Start()
    {
        if (isFirstTime == false)
        {
            menuAnims.enabled = true;
            return;
        }
        StartCoroutine(ChangeSplash());
    }
    private System.Collections.IEnumerator ChangeSplash()
    {
        transform.localScale = Vector3.one;
        yield return LocalizationSettings.InitializationOperation;

        //FADE IN
        yield return new WaitForSeconds(2 * Time.deltaTime);
        cs.FadeCanvasGroup(cs.gameObject.GetComponent<CanvasGroup>(), 0, 1, 2);

        //FADE OUT
        yield return new WaitUntil(() => cs.gameObject.GetComponent<CanvasGroup>().alpha == 1);
        transform.localScale = Vector3.zero;
        cs.FadeCanvasGroup(cs.gameObject.GetComponent<CanvasGroup>(), 1, 0, 2);

        //Menu & First Time
        menuAnims.enabled = true;
        isFirstTime = false;
        BannerAds.HideCall.Invoke();
        BannerAds.ShowBanner();
    }
}
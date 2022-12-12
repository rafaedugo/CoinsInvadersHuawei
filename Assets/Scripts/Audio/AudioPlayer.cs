using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    private Scene scene;
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        switch (scene.buildIndex)
        {
            case 0:
                StartCoroutine(playMusic());         
                AudioManager.Instance.Stop("GameplayTheme", AudioManager.Instance.music);
                AudioManager.Instance.Stop("FedTheme", AudioManager.Instance.music);
                AudioManager.Instance.Stop("Helicoptero", AudioManager.Instance.sounds);
                break;
            case 5:
                AudioManager.Instance.Stop("Helicoptero", AudioManager.Instance.sounds);
                AudioManager.Instance.Stop("FedTheme", AudioManager.Instance.music);
                AudioManager.Instance.UnPause("MenuTheme", AudioManager.Instance.music);
                break;
        }
    }

    private System.Collections.IEnumerator playMusic()
    {
        yield return new WaitUntil(() => SplashScreen.isFirstTime == false);
        AudioManager.Instance.Stop("MenuTheme", AudioManager.Instance.music);
        AudioManager.Instance.Play("MenuTheme", AudioManager.Instance.music);
    }
}
using UnityEngine;

public class Creditos : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.Instance.Play("GameplayTheme", AudioManager.Instance.music);
        AudioManager.Instance.Stop("MenuTheme", AudioManager.Instance.music);
    }
    private void OnDisable()
    {
        AudioManager.Instance.Stop("GameplayTheme", AudioManager.Instance.music);
        AudioManager.Instance.Play("MenuTheme", AudioManager.Instance.music);

    }
}
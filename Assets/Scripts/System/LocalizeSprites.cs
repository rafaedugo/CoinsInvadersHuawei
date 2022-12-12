using UnityEngine;
using UnityEngine.UI;

public class LocalizeSprites : MonoBehaviour
{
    private Image currentImage;
    [SerializeField] private Sprite Spanish;
    [SerializeField] private Sprite English;

    private void Awake() => currentImage = GetComponent<Image>();

    private void OnEnable()
    {
        switch(SaveSystem.data.language)
        {
            case 0: currentImage.sprite = English; break;
            case 1: currentImage.sprite = Spanish; break;
        }
        currentImage.SetNativeSize();
    }
}
using UnityEngine;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();

        if(gameObject.name == "MusicSlider")
            slider.onValueChanged.AddListener(FindObjectOfType<AudioManager>().SetMusicVolume);
        else if(gameObject.name == "SoundSlider")
            slider.onValueChanged.AddListener(FindObjectOfType<AudioManager>().SetSoundVolume);
    }
}
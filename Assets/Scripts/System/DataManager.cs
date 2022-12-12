using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable] public class Data
{
    public float soundVolume = .5f;
    public float musicVolume = .5f;

    public bool hasDiedFirstTime;
    public bool gameEnded;

    public int language = -1;

    public int totalScore;
    public int[] individualScores = new int[5];

    public bool[] achievements = new bool[7];
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    [SerializeField] private Slider music, sound;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        SaveSystem.Load();
        if(SaveSystem.data.language >= 0)
        {
            FindObjectOfType<LanguagesManager>().SetLanguage(SaveSystem.data.language);
        }
        
    }

    private void Start() => AsignVolumes();

    private void AsignVolumes()
    {
        music.value = SaveSystem.data.musicVolume;
        sound.value = SaveSystem.data.soundVolume;
        AudioManager.Instance.SetMusicVolume(SaveSystem.data.musicVolume);
        AudioManager.Instance.SetSoundVolume(SaveSystem.data.soundVolume);
    }

    public void SaveSettingsValues()
    {
        SaveSystem.data.musicVolume = GameObject.Find("MusicSlider").GetComponent<Slider>().value;
        SaveSystem.data.soundVolume = GameObject.Find("SoundSlider").GetComponent<Slider>().value;
        SaveSystem.Save();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) // 0 - Menu
            GameManager.accumulatedScore = 0;
    }

    #region resets
    [ContextMenu("Reset All Defaults")]
    public void ResetAllDefaults()
    {
        SaveSystem.data.musicVolume = .5f;
        SaveSystem.data.soundVolume = .5f;

        SaveSystem.data.language = 1;

        SaveSystem.data.hasDiedFirstTime = false;
        SaveSystem.data.gameEnded = false;

        SaveSystem.data.totalScore = 0;

        for(int i = 0; i < SaveSystem.data.individualScores.Length; i++)
            SaveSystem.data.individualScores[i] = 0;

        for (int i = 0; i < SaveSystem.data.achievements.Length; i++)
            SaveSystem.data.achievements[i] = false;

        SaveSystem.Save();
    }

    [ContextMenu("Reset Achievements")]
    public void ResetAchievements()
    {
        for (int i = 0; i < SaveSystem.data.achievements.Length; i++)
            SaveSystem.data.achievements[i] = false;

        SaveSystem.Save();
    }

    [ContextMenu("Reset Settings")]
    public void ResetSettings()
    {
        SaveSystem.data.musicVolume = .5f;
        SaveSystem.data.soundVolume = .5f;
        SaveSystem.Save();
    }
    #endregion

    private void OnApplicationQuit() => SaveSystem.Save();
}

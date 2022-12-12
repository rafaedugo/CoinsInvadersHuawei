using UnityEngine;
using System.Collections;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using System;
using UnityEngine.Android;
public class LanguagesManager : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private AchievementScreen[] achieves;
    private void Awake()
    {
        /*Debug.Log(Application.systemLanguage + " " + LocalizationSettings.SelectedLocale.LocaleName);
        string root = @"C:\users";
        string root2 = @"C:\Users";
        bool result = root.Equals(root2);
        if(LocalizationSettings.SelectedLocale.LocaleName.Contains(Application.systemLanguage.ToString()))
        {
            Debug.Log(LocalizationSettings.AvailableLocales.Locales[1].ToString() +" = " + Application.systemLanguage.ToString());
        }
           //Debug.Log(LocalizationSettings.AvailableLocales.GetLocale())
        //if(apl)
        Debug.Log($"Ordinal comparison: <{root}> and <{root2}> are {(result ? "equal." : "not equal.")}");

        result = root.Equals(root2, StringComparison.Ordinal);
        Debug.Log($"Ordinal comparison: <{root}> and <{root2}> are {(result ? "equal." : "not equal.")}");

        Debug.Log($"Using == says that <{root}> and <{root2}> are {(root == root2 ? "equal" : "not equal")}");*/
    }
    public void SetLanguage(int i) => StartCoroutine(setLanguage(i));
    private IEnumerator setLanguage(int i)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
        SaveSystem.data.language = i;
        SaveSystem.Save();
    }
    public void AsignLanguageOptions() => dropdown.value = SaveSystem.data.language;
    public void AsignLanguageAchievements()
    {
        foreach (AchievementScreen achieve in achieves)
        {
            switch (SaveSystem.data.language)
            {
                case 0:
                    achieve.display.title.text = achieve.achievement.title[0];
                    achieve.display.description.text = achieve.achievement.decription[0]; // 0 - Inglés
                    break;
                case 1:
                    achieve.display.title.text = achieve.achievement.title[1];
                    achieve.display.description.text = achieve.achievement.decription[1]; // 1 - Español
                    break;
            }
        } 
    }
}
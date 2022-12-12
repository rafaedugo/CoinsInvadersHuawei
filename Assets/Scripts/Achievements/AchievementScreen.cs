using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public class AchievementDisplay
{
    public Text title;
    public Text description;
    public Image imageMode;
    public Image bar;
}

public class AchievementScreen : MonoBehaviour
{
    public Achievement achievement;
    public AchievementDisplay display;
    [SerializeField] private Text status;

    private void Start()
    {
       // Debug.Log(SaveSystem.data.hasDiedFirstTime + " / " + SaveSystem.data.achievements[5]);

        switch (achievement.name)
        {
            case "Etherium" : AsignAchieveForEnemiesKilled(0, false, 500); break; //0 - Etherium
            case "Tether" : AsignAchieveForEnemiesKilled(1, false, 500); break;   //1 - Tether
            case "Libra" : AsignAchieveForEnemiesKilled(2, false, 500); break;    //2 - Libra
            case "Doge" : AsignAchieveForEnemiesKilled(3, false, 500); break;     //3 - Doge
            case "All": AsignAchieveForEnemiesKilled(4, true, 2500); break;       //4 - All
            case "nft": AsignAchieve(5); break;
            case "GameCompleted": AsignAchieve(6); break;   
        }
    }

    private void AsignAchieve(int achievementIndex)
    {
        if (SaveSystem.data.achievements[achievementIndex])
            display.bar.fillAmount = 1;
        else display.bar.fillAmount = 0;

        status.text = display.bar.fillAmount + " / 1";

        ImageChange(achievementIndex);
    }

    private void AsignAchieveForEnemiesKilled(int index, bool isTotal, int totalBarValue)
    {
        if(isTotal)
        {
            float total = 0;
            for (int i = 0; i < SaveSystem.data.individualScores.Length; i++)
                total += SaveSystem.data.individualScores[i];

            display.bar.fillAmount = total / totalBarValue;
            if (display.bar.fillAmount > 1)
                display.bar.fillAmount = 1;
            status.text = display.bar.fillAmount * totalBarValue + " / " + totalBarValue;
        } 
        else
        {
            display.bar.fillAmount = (float)SaveSystem.data.individualScores[index] / totalBarValue;
            if (display.bar.fillAmount > 1)
                display.bar.fillAmount = 1;
            status.text = display.bar.fillAmount * totalBarValue + " / " + totalBarValue;
        }

        ImageChange(index);
    }

    private void ImageChange(int index)
    {
        if (SaveSystem.data.achievements[index])
            display.imageMode.sprite = achievement.imageMode[1]; // 1 - Desbloqueado
        else  display.imageMode.sprite = achievement.imageMode[0]; // 0 - Bloqueado
    }
}
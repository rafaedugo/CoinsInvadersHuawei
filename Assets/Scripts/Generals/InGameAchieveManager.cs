using UnityEngine;
using System.Collections;

public class InGameAchieveManager : MonoBehaviour
{
    private bool[] stopLoop = new bool[7];
    [SerializeField] private Achievement[] achievements;
    [SerializeField] private AchievementDisplay display;
    [SerializeField] private Transform popTrans;
    private bool popping;

    private IEnumerator PopUpInfo(int index)
    {
        if(popping == false)
        {
            StartCoroutine(PopAchievement(index));
        }     
        else
        {
            yield return new WaitUntil(() => popping == false);
            StartCoroutine(PopAchievement(index));
        }  
    }

    private void UpdatePopInfo(int index)
    {
        switch (SaveSystem.data.language)
        {
            case 0:
                display.title.text = achievements[index].title[0];            // 0 - Inglés
                display.description.text = achievements[index].decription[0]; // 0 - Inglés
                break;
            case 1:
                display.title.text = achievements[index].title[1];            // 1 - Español
                display.description.text = achievements[index].decription[1]; // 1 - Español
                break;
        }
        display.imageMode.sprite = achievements[index].imageMode[2]; // 2 - Pop in game
    }
    private IEnumerator PopAchievement(int index)
    {
        UpdatePopInfo(index);
        popping = true;
        popTrans.LeanMoveLocalX(popTrans.localPosition.x + 695, 1).setEaseInBack();
        yield return new WaitForSeconds(4);
        popTrans.LeanMoveLocalX(popTrans.localPosition.x - 695, .5f).setEaseInBack();
        yield return new WaitForSeconds(.51f);
        popping = false;
    }

    private void Update()
    {
        if (!stopLoop[0]) CompareValues(0, false, 500); //0 - Etherium
        if (!stopLoop[1]) CompareValues(1, false, 500); //1 - Tether
        if (!stopLoop[2]) CompareValues(2, false, 500); //2 - Libra
        if (!stopLoop[3]) CompareValues(3, false, 500); //3 - Doge
        if (!stopLoop[4]) CompareValues(4, true, 2500); //4 - All
        if (!stopLoop[5]) CompareBooleanAchieves(5, SaveSystem.data.hasDiedFirstTime);
        if (!stopLoop[6]) CompareBooleanAchieves(6, SaveSystem.data.gameEnded);
    }

    private void CompareBooleanAchieves(int index, bool boolToCompare)
    {
        if (SaveSystem.data.achievements[index] == true)
        {
            stopLoop[index] = true;
            return;
        }

        if (boolToCompare)
        {
            SaveSystem.data.achievements[index] = true;
            StartCoroutine(PopUpInfo(index));
        }

        SaveSystem.Save();
    }

    private void CompareValues(int index, bool isTotal, int totalValue)
    {
        if(SaveSystem.data.achievements[index] == true)
        {
            stopLoop[index] = true;
            return;
        }

        if (isTotal)
        {
            float total = 0;
            for (int i = 0; i < SaveSystem.data.individualScores.Length; i++)
                total += SaveSystem.data.individualScores[i];

            if (total >= totalValue)
            {
                stopLoop[index] = true;
                SaveSystem.data.achievements[index] = true;
                StartCoroutine(PopUpInfo(index));
                Debug.LogFormat("Completed: {0}", achievements[index].name); 
            }  
        }
        else if (SaveSystem.data.individualScores[index] >= totalValue)
        {
            stopLoop[index] = true;
            SaveSystem.data.achievements[index] = true;
            StartCoroutine(PopUpInfo(index));
            Debug.LogFormat("Completed: {0}", achievements[index].name);
        }   
    }
}
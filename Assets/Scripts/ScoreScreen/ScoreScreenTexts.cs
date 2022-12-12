using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenTexts : MonoBehaviour
{
    [SerializeField] private Text[] individualScoreText;
    [SerializeField] private Text totalScoreText;

    private void Start()
    {
        for(int i = 0; i < individualScoreText.Length; i++)
            individualScoreText[i].text = SaveSystem.data.individualScores[i].ToString();

        totalScoreText.text = SaveSystem.data.totalScore.ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text txt;
    public Text[] panelsText;
    private void Update()
    {
        txt.text = GameManager.Instance.currentLevelScore + GameManager.accumulatedScore + "   ";
        foreach(Text txt in panelsText)
            txt.text = GameManager.Instance.currentLevelScore + GameManager.accumulatedScore + "";
    }
}

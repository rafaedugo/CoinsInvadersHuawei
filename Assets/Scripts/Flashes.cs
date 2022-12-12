using UnityEngine;
using UnityEngine.UI;

public class Flashes : MonoBehaviour
{
    public CanvasGroup canvas;
    public float flashTime;
    public void Flash() => StartCoroutine(flash());
    private System.Collections.IEnumerator flash()
    {
        LeanTween.alphaCanvas(canvas, .7f, flashTime);
        yield return new WaitUntil(() => canvas.alpha == .7f);
        LeanTween.alphaCanvas(canvas, 0, flashTime);
        yield return new WaitUntil(() => canvas.alpha == 0);
        LeanTween.alphaCanvas(canvas, .7f, flashTime);
        yield return new WaitUntil(() => canvas.alpha == .7f);
        LeanTween.alphaCanvas(canvas, 0, flashTime);
    }
}
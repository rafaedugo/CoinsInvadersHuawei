using UnityEngine;

public class PopScore : MonoBehaviour
{
    //private void Start() => StartCoroutine(showScore());
    private System.Collections.IEnumerator showScore()
    {
        LeanTween.alphaVertex(gameObject, 1, 0.3f);
        yield return new WaitForSeconds(1.3f);
        LeanTween.alphaVertex(gameObject, 0, 0.3f);
        yield return new WaitUntil(() => GetComponent<TMPro.TextMeshPro>().alpha == 0);
        Destroy(gameObject);
    }
}
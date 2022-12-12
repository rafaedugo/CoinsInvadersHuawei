using UnityEngine;

public class TweenedPopups : MonoBehaviour
{
    [SerializeField] [Range(0, 5)] private float tweenAnimationTime;

    public void AppearPopUps(Transform _transform)
    {
        _transform.localScale = Vector2.zero;
        _transform.LeanScale(Vector2.one, tweenAnimationTime);
    }

    public void DisappearPopUps(Transform _transform) =>
        _transform.LeanScale(Vector2.zero, tweenAnimationTime).setEaseInBack();

    public void CallDeactivatePanel(GameObject go) => StartCoroutine(DeactivatePanel(go));

    System.Collections.IEnumerator DeactivatePanel(GameObject go)
    {
        yield return new WaitForSeconds(tweenAnimationTime);
        go.SetActive(false);
    }
}

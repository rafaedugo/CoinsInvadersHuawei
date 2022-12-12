using UnityEngine;
using System.Collections;

public class HidePanelsWithKeys : MonoBehaviour
{
    public Transform _transform;
    private TweenedPopups tween;

    private void Awake() => tween = FindObjectOfType<TweenedPopups>();

    private void Update()
    {
        if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
            StartCoroutine(hideDelay());
    }

    private IEnumerator hideDelay()
    {
        Vector2 scale = _transform.localScale;
        yield return new WaitUntil(() => scale == Vector2.one);
        tween.DisappearPopUps(_transform);
        tween.CallDeactivatePanel(gameObject);
    }
}
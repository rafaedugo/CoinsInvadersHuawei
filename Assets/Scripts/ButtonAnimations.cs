using UnityEngine;
using System.Collections;

public class ButtonAnimations : MonoBehaviour
{
    private Vector3 initialSize;

    private void Awake()
    {
        initialSize = transform.localScale;
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(onClick);
    }
    public void onMouseOver() => transform.LeanScale(initialSize * 1.15f, .3f).setEaseOutBack();
    public void onClick() => AudioManager.Instance.Play("Buttons", AudioManager.Instance.sounds);
    public void onMouseExit() => transform.LeanScale(initialSize, .3f).setEaseInBack();
}
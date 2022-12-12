using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpCredits : MonoBehaviour
{
    public RectTransform trfm;
    public float spd;
    public float resetPos;
    // Start is called before the first frame update
    void Start()
    {
        trfm = gameObject.GetComponent<RectTransform>();
        trfm.localPosition = new Vector3(trfm.localPosition.x, resetPos, trfm.localPosition.z);
    }

    private void OnEnable()
    {
        trfm.localPosition = new Vector3(trfm.localPosition.x, resetPos, trfm.localPosition.z);
    }
    // Update is called once per frame
    void Update()
    {
        trfm.localPosition = new Vector3(trfm.localPosition.x, trfm.localPosition.y + (Time.deltaTime * spd), trfm.localPosition.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingFeedback : MonoBehaviour
{
    public float spd;
    public CanvasGroup canvas;
    float value = 1;
    float dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Flashing();
    }

    public void Flashing()
    {
        value += Time.deltaTime * spd * dir;

        if(value >=1)
        {
            value = 1;
            dir = dir * -1;
        }
        else if(value <=0)
        {
            value = 0;
            dir = dir * -1;
        }

        canvas.alpha = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnTrigerEv : MonoBehaviour
{

    public UnityEvent effectList;
    public string[] tagList;
    [HideInInspector]
    public GameObject whoCall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        for (int i = 0; i < tagList.Length; i++)
        {
            if (collision.gameObject.tag == tagList[i])
            {
                whoCall = collision.gameObject;
                CallEfects();
            }   
            else
            {
                Debug.Log(collision.gameObject.tag + " != " + tagList[i]);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == whoCall)
        {
            whoCall = null;
        }
    }

    private void CallEfects()
    {
        effectList.Invoke();

        gameObject.SetActive(false);
        Debug.Log("invoke");

    }
}

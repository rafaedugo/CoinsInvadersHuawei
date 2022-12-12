using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class LinkURLInvoker : LocalizedMonoBehaviour
{
    public string link;


    public void OpenLink()
    {
        Application.OpenURL(link);
    }

    public void ChangeLink(string newLink)
    {
        link = newLink;
    }
}

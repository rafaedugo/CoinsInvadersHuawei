using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypeVersion : MonoBehaviour
{
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt.text = "v. " + Application.version.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

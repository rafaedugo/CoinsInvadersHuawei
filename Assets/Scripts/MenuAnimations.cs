using UnityEngine;
using HmsPlugin;

[System.Serializable] public struct MenuElements
{
    public GameObject actualElement;
    public GameObject referenceForPos;
    [HideInInspector] public Vector3 initialPos;
    [HideInInspector] public Vector3 actualPos;
}

public class MenuAnimations : MonoBehaviour
{
    [SerializeField] private MenuElements[] elements;
    private string coinAnim;

    private void Start()
    {
        HMSAccountKitManager.Instance.SignIn();
        AnimateElements();
    }

    private void AnimateElements()
    {
        //Animations
        int index = Random.Range(0, 4);
        switch(index)
        {
            case 0: coinAnim = "Etherium@Entry"; break;
            case 1: coinAnim = "Tether@Entry"; break;
            case 2: coinAnim = "Libra@Entry"; break;
            case 3: coinAnim = "Doge@Entry"; break;
        }
        GameObject.Find("MenuAnimations").GetComponent<Animator>().Play(coinAnim);

        for (int i = 0; i < elements.Length; i++)
        {
            //Get the real position
            elements[i].actualPos = elements[i].actualElement.transform.position;

            //Get the reference initial position
            elements[i].initialPos = elements[i].referenceForPos.transform.position;

            //Asign the object initial position
            elements[i].actualElement.transform.position = elements[i].initialPos;

            //Animate movement to real position
            elements[i].actualElement.LeanMove(elements[i].actualPos, 2).setEaseInOutBack();
        }
    }
}
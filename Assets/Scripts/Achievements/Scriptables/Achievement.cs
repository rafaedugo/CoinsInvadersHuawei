using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
public class Achievement : ScriptableObject
{
    public string[] title;
    public string[] decription;
    public Sprite[] imageMode;
}

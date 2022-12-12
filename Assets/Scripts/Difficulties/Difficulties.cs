using UnityEngine;

[CreateAssetMenu(fileName = "New difficulty", menuName = "Difficulty")]
public class Difficulties : ScriptableObject
{
    public AnimationCurve enemiesSpeed;
    public float enemiesSpeedMultiplier;
    public float shootProbabilityMultiplier;
    public int columns;
    public RowOrder[] level0;
    public RowOrder[] level1;
    public RowOrder[] level2;
    public RowOrder[] level3;
}
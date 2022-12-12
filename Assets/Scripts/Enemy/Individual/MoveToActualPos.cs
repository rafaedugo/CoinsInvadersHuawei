using UnityEngine;

public class MoveToActualPos : MonoBehaviour
{
    private EnemiesGridController gridController;
    void Awake() => gridController = GetComponentInParent<EnemiesGridController>();
    void Start() => transform.localPosition = gridController.enemySpawnPositions[Random.Range(0, gridController.enemySpawnPositions.Length)].position;

    void FixedUpdate()
    {
       Vector3 actualPosition = Vector3.Lerp(transform.localPosition, Vector2.zero, Time.fixedDeltaTime * 1.8f);
       transform.localPosition = actualPosition;

       if (Vector3.Distance(transform.localPosition, Vector2.zero) <= .02f) this.enabled = false;
    }
}

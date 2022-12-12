using UnityEngine;

public class HelicopSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Fed;
    [SerializeField] private Transform[] spawnPositions;
    public System.Action fedKilled;

    private void Awake() => fedKilled += OnFedKilled;
    private void OnDisable() => fedKilled -= OnFedKilled;
    private void Start() => SpawnFed();

    private void SpawnFed()
    {
        GameObject go = Instantiate(Fed);
        go.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
    }

    private void OnFedKilled() => SpawnFed();
}

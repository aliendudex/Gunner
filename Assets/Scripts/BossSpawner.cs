using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform enemyContainer;

    private void Start()
    {
        if (bossPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Boss prefab or spawn point is missing!");
            return;
        }

        GameObject boss = Instantiate(
            bossPrefab,
            spawnPoint.position,
            spawnPoint.rotation,
            enemyContainer
        );
    }
}
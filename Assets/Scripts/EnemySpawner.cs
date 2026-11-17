using ED262C;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
     [SerializeField] private EnemyFactory enemyFactory;
     [SerializeField] private Transform enemyContainer;
     [SerializeField] private int numberOfEnemies = 5;

     private Camera mainCamera;

     private void Start()
    {
         mainCamera = Camera.main;
         SpawnEnemies();
    }

     private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            string enemyType = GetRandomEnemyType();
            Vector3 spawnPosition = GetRandomSpawnPosition();

            Debug.Log("Spawning enemy: " + enemyType + " at " + spawnPosition);

            enemyFactory.CreateEnemy(enemyType, spawnPosition, enemyContainer);
        }
    }

     private string GetRandomEnemyType()
     {
         int randomNumber = Random.Range(0, 3);

         if (randomNumber == 0)
        {
            return "Goblin";
        }
            else if (randomNumber == 1)
        {
            return "Archer";
        }
        else
        {
            return "Knight";
        }
     }

    private Vector3 GetRandomSpawnPosition()
    {
        RoomNavigation navigation = FindFirstObjectByType<RoomNavigation>();

        if (navigation == null)
        {
            Debug.LogError("RoomNavigation not found!");
            return Vector3.zero;
        }

        return navigation.GetRandomWalkablePosition();
    }
}
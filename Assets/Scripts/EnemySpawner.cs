using ED262C;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject goblinPrefab;
    private SimpleArrayList<Enemy> enemies;

    private void Start()
    {
        enemies = new SimpleArrayList<Enemy>();

        AddEnemy("Goblin");
        AddEnemy("Goblin");
        AddEnemy("Archer");
        AddEnemy("Knight");

        ShowEnemies();
    }

    private void AddEnemy(string enemyName)
    {
        Enemy enemy = new Enemy(enemyName);
        enemies.Add(enemy);

        if(enemyName == "Goblin")
        {
            Instantiate(goblinPrefab);
        }
    }

    private void ShowEnemies()
    {
        Debug.Log("=== ENEMIGOS DEL SPAWNER ===");

        for (int i = 0; i < enemies.Count; i++)
        {
            Debug.Log("Enemigo " + i + ": " + enemies[i]);
        }

        Debug.Log("Total de enemigos: " + enemies.Count);
    }
}
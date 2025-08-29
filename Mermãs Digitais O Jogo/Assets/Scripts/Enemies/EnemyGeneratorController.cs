using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    public EnemySpawner spawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawner.SpawnEnemies();
        }
    }
}

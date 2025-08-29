using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab do inimigo
    public int enemyCount = 15; // Quantos inimigos vão aparecer
    public float jumpForce; // Força do pulo
    public float horizontalForce = 3f; // Força para ir para a direita
    public float spawnInterval = 0.3f; // Intervalo entre cada inimigo
    public BoxCollider2D playerCollider;

    

    private bool hasSpawned = false;

    public void SpawnEnemies()
    {
        if (hasSpawned) return; // Garante que só acontece uma vez
        hasSpawned = true;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            enemyPrefab.SetActive(true);
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            BoxCollider2D enemyCollider = enemy.GetComponent<BoxCollider2D>();
            if (playerCollider != null && enemyCollider != null)
            {
                Physics2D.IgnoreCollision(enemyCollider, playerCollider);
            }

            // Aplica uma força para pular para a direita
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(new Vector2(horizontalForce, jumpForce), ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        PlayerCutsceneExit exitScript = FindFirstObjectByType<PlayerCutsceneExit>();
        if (exitScript != null)
        {
            exitScript.StartExit();
        }
    }


}

using System.Collections;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    public EnemySpawner spawner;
    [SerializeField] private Loader loader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawner.SpawnEnemies();
        }
    }

    private void Update()
    {
        StartCoroutine(SkipCutscene());
    }

    private IEnumerator SkipCutscene()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            yield return new WaitForSeconds(0.01f);
            loader.CarregarFase("Level Selector");
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    // [SerializeField] private string sceneName;
    private string cenaAtiva;
    [SerializeField] private bool[] levelComplete;
    public bool[] LevelComplete { get => levelComplete; set => levelComplete = value; }
    private int length = 1;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (FindAnyObjectByType<Transition>().length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        cenaAtiva = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cenaAtiva = SceneManager.GetActiveScene().name;

            if (cenaAtiva == "Tutorial")
            {
                levelComplete[0] = true;
            } else if (cenaAtiva == "Fase 1")
            {
                levelComplete[1] = true;
            } else if (cenaAtiva == "Fase 2")
            {
                levelComplete[2] = true;
            }

            SceneManager.LoadScene("Level Complete");
        }
    }
}

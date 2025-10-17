using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    // [SerializeField] private string sceneName;
    private string cenaAtiva;
    [SerializeField] private bool[] levelComplete = new bool[3];
    public bool[] LevelComplete { get => levelComplete; set => levelComplete = value; }
    // private int length = 1;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        // var transitions = FindObjectsOfType<Transition>(FindObjectsSortMode.None);
        /*if (FindObjectsByType<Transition>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
        }*/

        /*var transition0 = GameObject.Find("Transition0");
            if (cenaAtiva == "Fase 1")
            {
                Destroy(transition0.gameObject);
            }*/
    }

    private void Start()
    {
        cenaAtiva = SceneManager.GetActiveScene().name;

        var transition0 = GameObject.Find("Transition0");
        var transition1 = GameObject.Find("Transition1");
        var transition2 = GameObject.Find("Transition2");
        
        if (cenaAtiva == "Fase 1")
        {
            Destroy(transition0.gameObject);
        } else if (cenaAtiva == "Fase 2")
        {
            Destroy(transition1.gameObject);
        } else if (cenaAtiva == "Fase 3")
        {
            Destroy(transition2.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cenaAtiva = SceneManager.GetActiveScene().name;

            if (levelComplete == null || levelComplete.Length < 3)
            {
                levelComplete = new bool[3];
            }

            if (cenaAtiva == "Tutorial")
            {
                levelComplete[0] = true;
            } else if (cenaAtiva == "Fase 1")
            {
                levelComplete[0] = true;
                levelComplete[1] = true;
            } else if (cenaAtiva == "Fase 2")
            {
                levelComplete[0] = true;
                levelComplete[1] = true;
                levelComplete[2] = true;
            }

            SceneManager.LoadScene("Level Complete");
        }
    }
}

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
        //if (FindObjectsByType<Transition>(FindObjectsSortMode.None).Length > 1)
        //{
        //    Destroy(gameObject);
        //}

        //var transition0 = GameObject.Find("Transition0");
        //    if (cenaAtiva == "Fase 1")
        //    {
        //        Destroy(transition0.gameObject);
        //    }
    }

    private void Start()
    {
        int count = 0;
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Transition0")
            {
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                }
                count++;
            }
        }
        if (count > 1)
        {
            Destroy(gameObject);
            return;
        }

        count = 0;

        foreach (GameObject obj2 in allObjects)
        {
            if (obj2.name == "Transition1")
            {
                if (!obj2.activeSelf)
                {
                    obj2.SetActive(true);
                }
                count++;
            }
        }
        if (count > 1)
        {
            Destroy(gameObject);
            return;
        }

        count = 0;

        foreach (GameObject obj3 in allObjects)
        {
            if (obj3.name == "Transition2")
            {
                if (!obj3.activeSelf)
                {
                    obj3.SetActive(true);
                }
                count++;
            }
        }
        if (count > 1)
        {
            Destroy(gameObject);
            return;
        }

        /*cenaAtiva = SceneManager.GetActiveScene().name;

        //var transition0 = GameObject.Find("Transition0");
        //var transition1 = GameObject.Find("Transition1");
        //var transition2 = GameObject.Find("Transition2");

        if (transition1 != null || transition2 != null)
        {
            Destroy(transition0.gameObject);
        }
        else if (transition1 != null || transition2 != null)
        {
            Destroy(transition1.gameObject);
        }
        else if (cenaAtiva == "Fase 3" && transition2 != null)
        {
            Destroy(transition2.gameObject);
        }*/
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

            if (cenaAtiva == "Tutorial" && GameObject.Find("Transition1") == null && GameObject.Find("Transition2") == null)
            //if (cenaAtiva == "Tutorial")
            {
                levelComplete[0] = true;
            }
            else if (cenaAtiva == "Fase 1" && GameObject.Find("Transition2") == null)
            //else if (cenaAtiva == "Fase 1")
            {
                levelComplete[0] = true;
                levelComplete[1] = true;
            }
            else if (cenaAtiva == "Fase 2")
            {
                levelComplete[0] = true;
                levelComplete[1] = true;
                levelComplete[2] = true;
            }
            
            verificarMaiorTransition();

            SceneManager.LoadScene("Level Complete");
        }
    }
    
    private void verificarMaiorTransition()
    {
        GameObject transition1 = null;
        GameObject transition2 = null;
        GameObject transition3 = null;

        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Transition1") transition1 = obj;
            if (obj.name == "Transition2") transition2 = obj;
            if (obj.name == "Transition3") transition3 = obj;
        }

        // Define qual é o maior transition presente
        GameObject highestTransition = null;

        if (transition3 != null)
        {
            highestTransition = transition3;
        }
        else if (transition2 != null)
        {
            highestTransition = transition2;
        }
        else if (transition1 != null)
        {
            highestTransition = transition1;
        }

        // Se existe um maior, e não é este objeto, ativa ele e desativa este
        if (highestTransition != null)
        {
            if (highestTransition != gameObject)
            {
                highestTransition.SetActive(true);
                Debug.Log($"Ativando {highestTransition.name} e desativando {gameObject.name}");
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log($"{gameObject.name} já é o maior transition. Mantido ativo.");
            }
        }
        else
        {
            Debug.LogWarning("Nenhum Transition (1, 2 ou 3) foi encontrado.");
        }
    }
}

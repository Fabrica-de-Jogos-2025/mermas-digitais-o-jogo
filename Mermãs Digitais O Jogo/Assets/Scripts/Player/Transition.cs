using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    // [SerializeField] private string sceneName;
    private string cenaAtiva;
    [SerializeField] private bool[] levelComplete = new bool[3];
    public bool[] LevelComplete { get => levelComplete; set => levelComplete = value; }
    // private int length = 1;
    public bool levelConcluido = false;
    public bool tutorialCompletado = false;
    public bool fase1Completada = false;
    public bool fase2Completada = false;
    public bool fase3Completada = false;
    public bool validacaoPorStart = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        VerificacaoDoQueJaFoiConcluido();
        
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

            if (cenaAtiva == "Tutorial" && GameObject.Find("Transition1") == null && GameObject.Find("Transition2") == null && GameObject.Find("Transition3") == null)
            //if (cenaAtiva == "Tutorial")
            {
                levelComplete[0] = true;
                tutorialCompletado = true;
            }
            else if (cenaAtiva == "Boss Fase 1" && GameObject.Find("Transition2") == null && GameObject.Find("Transition3") == null)
            //else if (cenaAtiva == "Fase 1")
            {
                levelComplete[0] = true;
                levelComplete[1] = true;
                fase1Completada = true;
            }
            else if (cenaAtiva == "Boss Fase 2" && GameObject.Find("Transition3") == null)
            {
                levelComplete[0] = true;
                levelComplete[1] = true;
                levelComplete[2] = true;
                fase2Completada = true;
            }
            else if (cenaAtiva == "Boss Fase 3")
            {
                levelComplete[0] = true;
                levelComplete[1] = true;
                levelComplete[2] = true;
                fase3Completada = true;
            }

            verificarMaiorTransition();
            
            if (cenaAtiva == "Fase 1")
            {
                SceneManager.LoadScene("Boss Fase 1");
            }
            else if (cenaAtiva == "Fase 2")
            {
                SceneManager.LoadScene("Boss Fase 2");
            }
            else if (cenaAtiva == "Fase 3")
            {
                SceneManager.LoadScene("Boss Fase 3");
            }
            else
            {
                levelConcluido = true;
                SceneManager.LoadScene("Level Complete");
            }
        }
    }

    public void verificarMaiorTransition()
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

        if (validacaoPorStart)
        {
            if (fase3Completada)
            {
                Debug.Log("fase3Completada true");
            }
            else if (fase2Completada)
            {
                Debug.Log("fase2Completada true");
            }
            else if (fase1Completada)
            {
                Debug.Log("fase1Completada true");
            }
            else if (tutorialCompletado)
            {
                Debug.Log("tutorialCompletado true");
            }

            validacaoPorStart = false;
        }
        else
        // Se existe um maior, e não é este objeto, ativa ele e desativa este
        if (highestTransition != null)
        {
            if ((highestTransition != gameObject) && (cenaAtiva != "Tutorial" || cenaAtiva != "Fase 1" || cenaAtiva != "Fase 2" || cenaAtiva != "Fase 3"))
            {
                gameObject.SetActive(false);
                Debug.Log($"Ativando {highestTransition.name} e desativando {gameObject.name}");
                highestTransition.SetActive(true);
            }

            else if (cenaAtiva == "Tutorial" || cenaAtiva == "Fase 1" || cenaAtiva == "Fase 2" || cenaAtiva == "Fase 3")
            {
                Debug.Log("cena ativa é Tutorial ou Fase 1 ou Fase 2 ou Fase 3");
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
    
    void VerificacaoDoQueJaFoiConcluido()
    {
        GameObject[] allTransitions = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject transition in allTransitions)
        {
            
            // Tenta pegar o componente Transition do GameObject
            Transition t = transition.GetComponent<Transition>();
            if (t == null) continue; // se não tiver o script Transition, pula

            if (transition.name == "Transition0")
            {
                if (t.tutorialCompletado)
                {
                    tutorialCompletado = true;
                    levelComplete[0] = true;
                }
            }
            else
            if (transition.name == "Transition1")
            {
                if (t.fase1Completada)
                {
                    fase1Completada = true;
                    levelComplete[0] = true;
                    levelComplete[1] = true;
                }
            }
            else
            if (transition.name == "Transition2")
            {
                if (t.fase2Completada)
                {
                    fase2Completada = true;
                    levelComplete[0] = true;
                    levelComplete[1] = true;
                    levelComplete[2] = true;
                }
            }
            else
            if (transition.name == "Transition3")
            {
                if (t.fase3Completada)
                {
                    fase3Completada = true;
                    levelComplete[0] = true;
                    levelComplete[1] = true;
                    levelComplete[2] = true;
                }
            }
        }
    }   
}

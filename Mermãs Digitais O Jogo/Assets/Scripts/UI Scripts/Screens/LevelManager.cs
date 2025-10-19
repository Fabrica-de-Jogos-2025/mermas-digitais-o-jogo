using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private Loader loader;
    [SerializeField] private GameplayAudio sfxClick;
    [SerializeField] private AudioClip clip;

    public static string proximaCena;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantém só este objeto

            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this) 
        {
            Destroy(gameObject); // Evita duplicatas
        }
    }

    public void IrParaFase(string nomeCena)
    {
        proximaCena = nomeCena;
        instance.StartCoroutine(GoToCourse(nomeCena));
    }

    private IEnumerator GoToCourse(string nomeCena)
    {
        sfxClick.Audio(clip);
        yield return new WaitForSeconds(0.5f);
        loader.CarregarFase("Level Animation");
        // yield return new WaitForSeconds(1f);

        /*var tituloFase = GameObject.Find("LevelTitle")?.GetComponent<TextMeshProUGUI>();
        var nomeFase = GameObject.Find("LevelName")?.GetComponent<TextMeshProUGUI>();

        if (tituloFase != null && nomeFase != null)
        {
            if (proximaCena == "Tutorial")
            {
                tituloFase.text = "TUTORIAL";
                nomeFase.text = "";
            }
            else if (proximaCena == "Fase 1")
            {
                tituloFase.text = "FASE 1";
                nomeFase.text = "Aprendendo a ser lógica";
            } else if (proximaCena == "Fase 2")
            {
                tituloFase.text = "FASE 2";
                nomeFase.text = "Variando e Desviando";
            } else if (proximaCena == "Fase 3")
            {
                tituloFase.text = "FASE 3";
                nomeFase.text = "Funções e Repetições";
            }
        }
        else
        {
            Debug.LogWarning("Não encontrei LevelTitle ou LevelName na cena Level Animation!");
        }*/


        yield return new WaitForSeconds(10f);
        loader.CarregarFase(proximaCena);
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        // Quando a cena "Level Animation" for carregada

        if (cena.name == "Level Animation")
        {
            // Agora os objetos já existem na hierarquia
            var tituloFase = GameObject.Find("LevelTitle")?.GetComponent<TextMeshProUGUI>();
            var nomeFase = GameObject.Find("LevelName")?.GetComponent<TextMeshProUGUI>();

            if (tituloFase != null && nomeFase != null)
            {
                if (proximaCena == "Tutorial")
                {
                    tituloFase.text = "TUTORIAL";
                    nomeFase.text = "";
                }
                else if (proximaCena == "Fase 1")
                {
                    tituloFase.text = "FASE 1";
                    nomeFase.text = "Aprendendo a ser lógica";
                }
                else if (proximaCena == "Fase 2")
                {
                    tituloFase.text = "FASE 2";
                    nomeFase.text = "Variando e Desviando";
                }
                else if (proximaCena == "Fase 3")
                {
                    tituloFase.text = "FASE 3";
                    nomeFase.text = "Funções e Repetições";
                }
            }
            else
            {
                Debug.LogWarning("?? LevelTitle ou LevelName não foram encontrados na cena Level Animation!");
            }
        }
    }
}

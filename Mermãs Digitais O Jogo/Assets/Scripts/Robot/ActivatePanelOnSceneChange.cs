using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivatePanelOnSceneChange : MonoBehaviour
{
    public GameObject panelToActivate; // Arraste aqui no Inspector o painel que quer ativar

    private string lastSceneName;

    void Start()
    {
        // Guarda o nome da cena atual ao iniciar
        lastSceneName = SceneManager.GetActiveScene().name;

        // Deixa o painel desativado inicialmente, se quiser
        /*if (panelToActivate != null)
            panelToActivate.SetActive(false);*/

        // Registra o callback para quando uma nova cena carregar
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Se o nome da cena mudou, ativa o painel
        if (scene.name != lastSceneName)
        {
            if (panelToActivate != null && (SceneManager.GetActiveScene().name != "Morte_Falha"))
            {
                panelToActivate.SetActive(true);
            }

            lastSceneName = scene.name; // Atualiza o nome da cena atual
        }
    }

    void OnDestroy()
    {
        // Remove o callback para evitar vazamento de memória
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

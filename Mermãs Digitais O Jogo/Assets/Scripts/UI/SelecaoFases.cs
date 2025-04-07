using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelecaoFases : MonoBehaviour
{
    [Header("Botões das Fases")]
    public Button botaoTurorial;
    public Button botaoFase1;
    public Button botaoFase2;
    public Button botaoFase3;

    void Start()
    {
        // Adiciona os listeners para os botões
        if (botaoTurorial != null)
            botaoTurorial.onClick.AddListener(() => CarregarFase("Tutorial"));

        if (botaoFase1 != null)
        botaoFase1.onClick.AddListener(() => CarregarFase("Fase 1"));

        if (botaoFase2 != null)
            botaoFase2.onClick.AddListener(() => CarregarFase("Fase 2"));

        if (botaoFase3 != null)
            botaoFase3.onClick.AddListener(() => CarregarFase("Fase 3"));
    }

    public void CarregarFase(string nomeDaCena)
    {
        Debug.Log("Carregando: " + nomeDaCena);
        SceneManager.LoadScene(nomeDaCena);
    }
}

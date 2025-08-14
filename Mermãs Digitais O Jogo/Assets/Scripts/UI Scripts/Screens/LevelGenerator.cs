using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    [Header("Botões das Fases")]
    public Button botaoTurorial;
    public Button botaoFase1;
    public Button botaoFase2;
    public Button botaoFase3;

    void Start()
    {
        if (botaoTurorial != null)
            botaoTurorial.onClick.AddListener(() => CarregarComLoading("Tutorial"));

        if (botaoFase1 != null)
            botaoFase1.onClick.AddListener(() => CarregarComLoading("Fase 1"));

        if (botaoFase2 != null)
            botaoFase2.onClick.AddListener(() => CarregarComLoading("Fase 2"));

        if (botaoFase3 != null)
            botaoFase3.onClick.AddListener(() => CarregarComLoading("Fase 3"));
    }

    void CarregarComLoading(string nomeCena)
    {
        Debug.Log("Passando pela tela de carregamento antes de: " + nomeCena);
        Loader.CarregarFase(nomeCena); // Aqui ele chama o sistema de carregamento com animação
    }
}


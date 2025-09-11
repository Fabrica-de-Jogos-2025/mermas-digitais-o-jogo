using UnityEngine;

public class PontoDeEntrada : MonoBehaviour
{
    public string tagDoJogador = "Player";

    void Start()
    {
        GameObject jogador = GameObject.FindWithTag(tagDoJogador);

        if (jogador != null)
        {
            jogador.transform.position = transform.position;
        }
        else
        {
            Debug.LogWarning("Jogador não encontrado na cena!");
        }
    }
}

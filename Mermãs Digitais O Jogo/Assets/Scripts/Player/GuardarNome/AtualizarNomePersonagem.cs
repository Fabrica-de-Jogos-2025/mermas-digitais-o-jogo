using UnityEngine;
using TMPro;

public class AtualizarNomePersonagem : MonoBehaviour
{
    public TMP_Text nomeTexto;  // referencia para o TMP_Text do Character_Name

    void Start()
    {
        if (!string.IsNullOrEmpty(PlayerData.playerName))
        {
            nomeTexto.text = PlayerData.playerName;
        }/*
        else
        {
           nomeTexto.text = "Jogador"; // nome padrão caso não tenha sido definido
        }*/
    }
}

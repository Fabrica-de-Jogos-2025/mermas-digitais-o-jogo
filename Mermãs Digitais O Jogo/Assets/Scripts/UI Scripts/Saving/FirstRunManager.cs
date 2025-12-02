using UnityEngine;

public class FirstRunManager : MonoBehaviour
{
    void Awake()
    {
        // Verifica se é a primeira execução do jogo
        if (!PlayerPrefs.HasKey("firstRun"))
        {
            // Debug.Log("Primeira execução detectada. Limpando PlayerPrefs...");

            // Apaga todos os dados salvos anteriormente
            PlayerPrefs.DeleteAll();

            // Marca que o jogo já foi iniciado uma vez
            PlayerPrefs.SetInt("firstRun", 1);
            PlayerPrefs.Save();
        }
    }
}

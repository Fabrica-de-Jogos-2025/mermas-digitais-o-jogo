using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CarregarFaseSelecionada());
    }

    private System.Collections.IEnumerator CarregarFaseSelecionada()
    {
        // Espera 1 segundos pra mostrar a animação da personagem
        yield return new WaitForSeconds(1f);

        if (!string.IsNullOrEmpty(FaseLoader.NomeFaseDestino))
        {
            AsyncOperation carregando = SceneManager.LoadSceneAsync(FaseLoader.NomeFaseDestino);

            while (!carregando.isDone)
            {
                yield return null;
            }
        }
        else
        {
            Debug.LogError("Nenhuma fase foi selecionada pra carregar.");
        }
    }
}

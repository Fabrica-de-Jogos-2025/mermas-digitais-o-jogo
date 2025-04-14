using UnityEngine;
using UnityEngine.SceneManagement;

public class FaseLoader : MonoBehaviour
{
    private static string nomeDaCenaPraCarregar;
    private static bool carregandoFase = false;

    void Start()
    {
        if (carregandoFase)
        {
            carregandoFase = false;
            StartCoroutine(CarregarCena());
        }
    }

    public static void CarregarFase(string nomeCena)
    {
        nomeDaCenaPraCarregar = nomeCena;
        carregandoFase = true;
        SceneManager.LoadScene("Carregando");
    }

    private System.Collections.IEnumerator CarregarCena()
    {
        yield return new WaitForSeconds(2f); // tempo pra mostrar a animação
        SceneManager.LoadScene(nomeDaCenaPraCarregar);
    }
}

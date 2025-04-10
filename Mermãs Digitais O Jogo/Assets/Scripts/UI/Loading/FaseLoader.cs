using UnityEngine;
using UnityEngine.SceneManagement;

public static class FaseLoader
{
    public static string NomeFaseDestino;

    public static void CarregarFase(string nomeFase)
    {
        NomeFaseDestino = nomeFase;
        SceneManager.LoadScene("CenaDeCarregamento"); 
    }
}

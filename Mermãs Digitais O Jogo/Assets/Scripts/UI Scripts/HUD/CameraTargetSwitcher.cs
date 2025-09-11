using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraTargetSwitcher : MonoBehaviour
{
    public string personagemTag = "Player";
    public string nomeCenaQueSegue;
    private bool ligado = false;

    void Update()
    {
        if (SceneManager.GetActiveScene().name != nomeCenaQueSegue)
        {
            Destroy(this.gameObject);
        }

        if (ligado) return;

        GameObject personagem = GameObject.FindGameObjectWithTag(personagemTag);

        if (personagem != null)
        {
            transform.position = personagem.transform.position;
            transform.SetParent(personagem.transform);
            ligado = true;
        }
    }
}

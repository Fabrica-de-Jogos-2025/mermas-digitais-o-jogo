using UnityEngine;
// using UnityEngine.Tilemaps;

public class MainMenuGroundEffect : MonoBehaviour
{
    [SerializeField] private float velocidade;

    private Vector3 posicaoInicial;
    private float tamanhoRealdaImagem;

    private void Awake()
    {
        posicaoInicial = transform.position;
        float tamanhoDaImagem = GetComponent<SpriteRenderer>().size.x;
        float escala = transform.localScale.x;
        tamanhoRealdaImagem = tamanhoDaImagem * escala;
    }

    // Update is called once per frame
    void Update()
    {
        float deslocamento = Mathf.Repeat(velocidade * Time.time, tamanhoRealdaImagem);
        transform.position = posicaoInicial + Vector3.left * deslocamento;
    }
}

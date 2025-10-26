using UnityEngine;

public class MudarPosicaoDeObjeto : MonoBehaviour
{
    public GameObject objetoQueSeraAtivado;
    public float posicaoX, posicaoY, posicaoZ;
    private GameObject objetoAtivo = null;
    void Start()
    {
        if(objetoQueSeraAtivado != null)
        {
            objetoAtivo = objetoQueSeraAtivado;
        }
    }
    
    void Update()
    {
        if(objetoAtivo != null && objetoAtivo.activeSelf && gameObject.activeSelf)
        {
            transform.position = new Vector3(posicaoX, posicaoY, posicaoZ);
        }
    }
}

using UnityEngine;

public class Die5 : MonoBehaviour
{
    public Hability5 Dest;
    public float destroyInSec = 3f;
    private bool isDestroyed = false;
    public GameObject rocket, F; // Referência ao foguete
    [SerializeField] private GameObject rocketCollider; // Collider do foguete

    public Hability5 Dest2;
    
    void Start()
    {
        // Obtém o Collider do foguete no início do jogo
    }


    void Update()
    {

        if (Dest.IsCorrectlyPlaced())
        {
            StartCoroutine(DestroyAfterDelay());
        } 

        // Verifica se todas as peças estão corretamente posicionadas
        if (!isDestroyed && Hability5.allDraggableObjects.TrueForAll(obj => obj.IsCorrectlyPlaced()) && Dest2.puzzleSolved)
        {
            StartCoroutine(DestroyAfterDelay());
            isDestroyed = true; // Evita chamadas repetidas da coroutine
            isDestroyed = true;
            Destroy(this.gameObject);
            Destroy(rocketCollider.gameObject);
        }

    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyInSec);
        Destroy(this.gameObject);
        Destroy(F.gameObject);
    }
}

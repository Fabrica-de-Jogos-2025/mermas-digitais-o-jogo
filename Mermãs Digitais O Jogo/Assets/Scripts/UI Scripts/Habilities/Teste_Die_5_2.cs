using UnityEngine;

public class Teste_Die_5_2 : MonoBehaviour
{

    public Teste_Hability5_2 Dest;
    //public float destroyInSec = 3f;
    private bool isDestroyed = false;
    public GameObject rocket; // Referência ao foguete
    [SerializeField] private GameObject rocketCollider; // Collider do foguete

    
    void Update()
    {
        if (!isDestroyed && Dest.puzzleSolved)
        {
            //StartCoroutine(DestroyAfterDelay());
            isDestroyed = true; // Evita chamadas repetidas da coroutine
            //isDestroyed = true;
            Destroy(this.gameObject);
            Destroy(rocket.gameObject);
            Destroy(rocketCollider.gameObject);
        }
    }

    /*private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyInSec);
        Destroy(this.gameObject);
        Destroy(rocket.gameObject);
    }*/
}
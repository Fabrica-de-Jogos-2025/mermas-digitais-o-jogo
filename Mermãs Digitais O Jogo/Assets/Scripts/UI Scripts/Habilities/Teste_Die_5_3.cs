using UnityEngine;

public class Teste_Die_5_3 : MonoBehaviour
{

    public Teste_Hability5_3 Dest;
    //public float destroyInSec = 3f;
    private bool isDestroyed = false;
    //public GameObject rocket; // Referência ao foguete
    //[SerializeField] private GameObject rocketCollider; // Collider do foguete
    public GameObject habilityCard, Enemy5, Enemy6, Enemy7, Enemy8, hitbox;

    
    void Update()
    {
        if (!isDestroyed && Dest.puzzleSolved)
        {
            //StartCoroutine(DestroyAfterDelay());
            isDestroyed = true; // Evita chamadas repetidas da coroutine
            habilityCard.SetActive(true);
            //isDestroyed = true;
            Destroy(Enemy5.gameObject);
            Destroy(Enemy6.gameObject);
            Destroy(Enemy7.gameObject);
            Destroy(Enemy8.gameObject);

            Destroy(hitbox.gameObject);

            Destroy(this.gameObject);
            //Destroy(rocket.gameObject);
            //Destroy(rocketCollider.gameObject);
        }
    }

    /*private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyInSec);
        Destroy(this.gameObject);
        //Destroy(rocket.gameObject);
    }*/
}
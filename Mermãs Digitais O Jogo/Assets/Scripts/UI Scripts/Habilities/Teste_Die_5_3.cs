using UnityEngine;

public class Teste_Die_5_3 : MonoBehaviour
{
    private PlayerMovement player;
    public Teste_Hability5_3 Dest;
    //public float destroyInSec = 3f;
    private bool isDestroyed = false;
    //public GameObject rocket; // Referência ao foguete
    //[SerializeField] private GameObject rocketCollider; // Collider do foguete
    public GameObject habilityCard, Enemy5, Enemy6, Enemy7, Enemy8, hitbox;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hability;
    private bool hasPlayed = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!isDestroyed && Dest.puzzleSolved && !hasPlayed)
        {
            hasPlayed = true;
            //StartCoroutine(DestroyAfterDelay());
            isDestroyed = true; // Evita chamadas repetidas da coroutine
            sfxAcess.Audio(hability);
            player.IsFrozen = false;
            habilityCard.SetActive(true);
            //isDestroyed = true;
            Destroy(Enemy5.gameObject);
            Destroy(Enemy6.gameObject);
            Destroy(Enemy7.gameObject);
            Destroy(Enemy8.gameObject);

            Destroy(hitbox.gameObject);

            Destroy(this.gameObject, hability.length);
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
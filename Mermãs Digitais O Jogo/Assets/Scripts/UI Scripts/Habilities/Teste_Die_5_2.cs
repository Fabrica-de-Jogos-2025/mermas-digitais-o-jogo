using UnityEngine;

public class Teste_Die_5_2 : MonoBehaviour
{
    private PlayerMovement player;
    public Teste_Hability5_2 Dest;
    //public float destroyInSec = 3f;
    private bool isDestroyed = false;
    //public GameObject rocket; // Referência ao foguete
    [SerializeField] private GameObject rocketCollider; // Collider do foguete
    [SerializeField] private GameObject hitbox; // Collider da habilidade

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
            //isDestroyed = true;
            sfxAcess.Audio(hability);
            Destroy(this.gameObject, hability.length);
            player.IsFrozen = false;
            //Destroy(rocket.gameObject);
            Destroy(rocketCollider.gameObject);
            Destroy(hitbox.gameObject);
        }
    }

    /*private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyInSec);
        Destroy(this.gameObject);
        //Destroy(rocket.gameObject);
    }*/
}
using UnityEngine;
using System.Collections;

public class Item_Hability : MonoBehaviour
{
    public GameObject Hability;
    private PlayerMovement player;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip open;
    
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.IsFrozen = true;
            StartCoroutine(desabilitarAposFinalizarAudio());
        }
    }
    
    private IEnumerator desabilitarAposFinalizarAudio()
    {
        sfxAcess.Audio(open);
        yield return new WaitForSeconds(open.length);
        Hability.SetActive(true);
        Destroy(gameObject);
    }
}

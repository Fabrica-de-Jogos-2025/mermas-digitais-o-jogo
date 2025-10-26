using UnityEngine;

public class Item_Hability : MonoBehaviour
{
    public GameObject Hability;
    private PlayerMovement player;
    
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
            Hability.SetActive(true);
            Destroy(gameObject);
        }
    }
}

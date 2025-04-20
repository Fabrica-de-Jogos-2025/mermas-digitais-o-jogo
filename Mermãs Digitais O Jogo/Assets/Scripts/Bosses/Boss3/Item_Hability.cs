using UnityEngine;

public class Item_Hability : MonoBehaviour
{
    public GameObject Hability;
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Hability.SetActive(true);
            Destroy(gameObject);
        }
    }
}

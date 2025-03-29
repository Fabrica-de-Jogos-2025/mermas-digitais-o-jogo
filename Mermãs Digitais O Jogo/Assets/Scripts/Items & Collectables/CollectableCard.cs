using UnityEngine;

public class CollectableCard : MonoBehaviour
{
    private PlayerStatus quantityCards;

    private void Start()
    {
        //quantityCards = FindObjectOfType<PlayerStatus>();
        quantityCards = FindFirstObjectByType<PlayerStatus>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            quantityCards.Cards++;
            Destroy(gameObject);
        }
    }
}

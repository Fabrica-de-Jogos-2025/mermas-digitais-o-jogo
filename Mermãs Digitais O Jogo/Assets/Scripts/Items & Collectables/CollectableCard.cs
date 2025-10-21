using UnityEngine;

public class CollectableCard : MonoBehaviour
{
    private PlayerStatus quantityCards;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip card;

    public AudioClip Card { get => card; set => card = value; }

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
            sfxAcess.Audio(card);
            Destroy(gameObject, card.length);
        }
    }
}

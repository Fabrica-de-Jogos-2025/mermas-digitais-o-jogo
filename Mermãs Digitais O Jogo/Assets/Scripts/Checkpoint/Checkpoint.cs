using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Sprites dos Checkpoints (na ordem da loja)")]
    [SerializeField] private Sprite[] checkpointSprites;
    [SerializeField] private Sprite activatedCheckpoint;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip checkpoint;
    private bool isActivated = false;
    [SerializeField] private int equippedIndex;
    public bool IsActivated { get => isActivated; set => isActivated = value; }

    private void Start()
    {
        // ?? Aplica o sprite do checkpoint escolhido na loja
        equippedIndex = PlayerPrefs.GetInt("EquippedCheckpointIndex", -1);
        if (equippedIndex >= 0 && equippedIndex < checkpointSprites.Length)
        {
            GetComponent<SpriteRenderer>().sprite = checkpointSprites[equippedIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isActivated)
        {
            if (checkpoint != null && sfxAcess != null)
                sfxAcess.Audio(checkpoint);

            isActivated = true;

            if (activatedCheckpoint != null && (equippedIndex < 0 || equippedIndex > checkpointSprites.Length))
                GetComponent<SpriteRenderer>().sprite = activatedCheckpoint;

            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.SetLastCheckpoint(transform.position);
                PlayerPrefs.SetInt("CheckpointAtivo", 1);
                PlayerPrefs.SetFloat("CheckpointX", transform.position.x);
                PlayerPrefs.SetFloat("CheckpointY", transform.position.y);
                PlayerPrefs.SetFloat("CheckpointZ", transform.position.z);
                PlayerPrefs.Save();
            }
        }
    }
}

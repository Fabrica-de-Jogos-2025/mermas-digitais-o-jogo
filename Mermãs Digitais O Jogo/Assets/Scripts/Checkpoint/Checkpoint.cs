using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] private Sprite activatedCheckpoint;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip checkpoint;
    private bool isActivated = false;

    public bool IsActivated { get => isActivated; set => isActivated = value; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isActivated)
        {
            if (checkpoint != null && sfxAcess != null)
                sfxAcess.Audio(checkpoint);

            isActivated = true;

            if (activatedCheckpoint != null)
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
            /*PlayerMovement player = other.GetComponent<PlayerMovement>();

            if(player != null)
            {
                player.SetLastCheckpoint(this.transform.position);
                PlayerPrefs.SetInt("CheckpointAtivo", 1);
                PlayerPrefs.SetFloat("CheckpointX", transform.position.x);
                PlayerPrefs.SetFloat("CheckpointY", transform.position.y);
                PlayerPrefs.SetFloat("CheckpointZ", transform.position.z);
                PlayerPrefs.Save();
            }

            if(!isActivated && activatedCheckpoint != null){
                isActivated = true;
                GetComponent<SpriteRenderer>().sprite = activatedCheckpoint;

                if (isActivated)
                {
                    sfxAcess.StopAudio();
                }
            }*/
        }
    }
}

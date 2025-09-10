using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] private Sprite activatedCheckpoint;
    private bool isActivated = false;

    public bool IsActivated { get => isActivated; set => isActivated = value; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            
            PlayerMovement player = other.GetComponent<PlayerMovement>();

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
            }
        }
    }
}

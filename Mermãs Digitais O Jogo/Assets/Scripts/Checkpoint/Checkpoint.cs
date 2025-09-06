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
            }

            if(!isActivated && activatedCheckpoint != null){
                isActivated = true;
                GetComponent<SpriteRenderer>().sprite = activatedCheckpoint;
            }
        }
    }
}

using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] private Sprite activatedCheckpoint;
    private bool isActivated = false;
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isActivated && other.CompareTag("Player"))
        {
            isActivated = true;
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if(player != null)
            {
                player.SetLastCheckpoint(this.transform.position);
            }

            if(activatedCheckpoint != null){
                GetComponent<SpriteRenderer>().sprite = activatedCheckpoint;
            }
        }
    }
}

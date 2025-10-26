using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject hability;
    public bool pausarjogador = false;
    private bool i = true;
    public bool valid = false;
    private PlayerMovement player;
    
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }
        void Update()
    {
        if (hability.activeSelf && i)
        {
            pausarjogador = true;
            i = false;
        }
        else if (!hability.activeSelf && !i)
        {
            player.IsFrozen = false;
            pausarjogador = false;
            i = true;
            valid = true;
            Destroy(this.gameObject);
        }
    }
}

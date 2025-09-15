using UnityEngine;

public class Destroyer6_2Colider : MonoBehaviour
{
    private PlayerMovement player;
    public Hability6 h;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (h.UsoDaUltimaHabilidade)
        {
            Destroy(this.gameObject);
            player.IsFrozen = false;
        }
    }
}

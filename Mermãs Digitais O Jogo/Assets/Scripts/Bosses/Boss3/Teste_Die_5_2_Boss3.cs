using UnityEngine;

public class Teste_Die_5_2_Boss3 : MonoBehaviour
{

    public Teste_Hability5_2 Dest;
    private bool isDestroyed = false;
    public Boss3_Attack Z;
    private PlayerMovement player;
    
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }
    void Update()
    {
        if (!isDestroyed && Dest.puzzleSolved)
        {
            player.IsFrozen = false;
            isDestroyed = true;
            Z.z = true;
            Destroy(this.gameObject);
        }
    }
}
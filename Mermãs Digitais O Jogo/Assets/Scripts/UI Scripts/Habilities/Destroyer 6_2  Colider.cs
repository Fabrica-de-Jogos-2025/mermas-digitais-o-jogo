using UnityEngine;

public class Destroyer6_2Colider : MonoBehaviour
{   
    public Hability6 h;
    void Update()
    {
        if (h.UsoDaUltimaHabilidade)
        {
            Destroy(this.gameObject);
        }
    }
}

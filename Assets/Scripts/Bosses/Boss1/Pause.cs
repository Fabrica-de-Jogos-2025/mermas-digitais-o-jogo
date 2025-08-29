using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject hability;
    public bool pausarjogador = false;
    private bool i = true;
    public bool valid = false;
    
    void Start()
    {
        
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
            pausarjogador = false;
            i = true;
            valid = true;
            Destroy(this.gameObject);
        }
    }
}

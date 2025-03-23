using UnityEngine;

public class DiePlatform : MonoBehaviour
{
    public Colision Dest;

    private void Update()
    {
        if (Dest.valid)
        {
            Destroy(this.gameObject);
        }
    }
}

using UnityEngine;

public class die : MonoBehaviour
{
    public Hability1Use_1 Dest;

    private void Update()
    {
        if (Dest.destroy)
        {
            Destroy(this.gameObject);
        }
    }
}

using UnityEngine;

public class die_2_v2 : MonoBehaviour
{
    public Hability2OpenAndClose Dest;

    private void Update()
    {
        if (Dest.destroy)
        {
            Destroy(this.gameObject);
        }
    }
}

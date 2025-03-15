using UnityEngine;

public class die_2 : MonoBehaviour
{
    public Hability2OpenAndClose_2 Dest;

    private void Update()
    {
        if (Dest.destroy)
        {
            Destroy(this.gameObject);
        }
    }
}

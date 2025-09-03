using UnityEngine;

public class die : MonoBehaviour
{
    public Hability1Use_1 Dest;
    public Hability1Use_1 Dest2;
    public Hability1Use_1 Dest3;

    private void Update()
    {
        if (Dest.destroy || Dest2.destroy || Dest3.destroy)
        {
            Destroy(this.gameObject);
        }
    }
}

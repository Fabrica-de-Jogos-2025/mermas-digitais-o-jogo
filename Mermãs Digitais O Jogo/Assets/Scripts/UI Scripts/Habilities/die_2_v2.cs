using UnityEngine;

public class die_2_v2 : MonoBehaviour
{
    public Hability2OpenAndClose Dest;
    private bool scheduledDestroy = false;

    private void Update()
    {
        if (Dest.destroy && !scheduledDestroy)
        {
            scheduledDestroy = true;
            Destroy(this.gameObject, Dest.Feedback.length);
        }
    }
}

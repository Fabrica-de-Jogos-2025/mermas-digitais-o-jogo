using UnityEngine;

public class die_4_2 : MonoBehaviour
{

    public Hability4 Dest;
    public Hability4_1 Dest2;
    public GameObject signalObjectRed;
    public GameObject signalObjectYellow;
    public GameObject signalObjectGreen;
    public GameObject cage;
    public float destroyInSec = 3f;
    // Update is called once per frame
    void Update()
    {
        if (Dest.IsCorrectlyPlaced())
        {
            signalObjectRed.SetActive(false);
            signalObjectYellow.SetActive(true);
        }
        
        if (Dest.IsCorrectlyPlaced() && Dest2.IsCorrectlyPlaced())
        {
            signalObjectYellow.SetActive(false);
            signalObjectGreen.SetActive(true);
            Destroy(cage.gameObject);
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyInSec);
        Destroy(this.gameObject);
    }
}

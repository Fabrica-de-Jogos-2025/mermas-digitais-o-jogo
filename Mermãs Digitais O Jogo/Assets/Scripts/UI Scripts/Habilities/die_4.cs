using UnityEngine;

public class die_4 : MonoBehaviour
{

    public Hability4 Dest;
    public GameObject signalObjectRed;
    public GameObject signalObjectGreen;
    public float destroyInSec = 3f;
    // Update is called once per frame
    void Update()
    {
        if (Dest.IsCorrectlyPlaced())
        {
            signalObjectRed.SetActive(false);
            signalObjectGreen.SetActive(true);
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyInSec);
        Destroy(this.gameObject);
    }
}

using UnityEngine;

public class die_4 : MonoBehaviour
{

    public Hability4 Dest;
    public GameObject signalObjectRed;
    public GameObject signalObjectGreen;
    public GameObject wind;
    // public float destroyInSec = 3f;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hability;
    private bool hasPlayed = false;
    // Update is called once per frame
    void Update()
    {
        if (Dest.IsCorrectlyPlaced() && !hasPlayed)
        {
            hasPlayed = true;
            signalObjectRed.SetActive(false);
            Destroy(wind.gameObject);
            signalObjectGreen.SetActive(true);
            StartCoroutine(DestroyAfterDelay());
        } 
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        sfxAcess.Audio(hability);
        yield return new WaitForSeconds(hability.length);
        Destroy(this.gameObject);
    }
}

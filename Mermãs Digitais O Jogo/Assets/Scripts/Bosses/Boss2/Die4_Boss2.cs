using UnityEngine;
using System.Collections;

public class Die4_Boss2 : MonoBehaviour
{
    public Hability4 Dest;
    public Hability4_1 Dest2;
    public GameObject signalObjectRed;
    public GameObject signalObjectYellow;
    public GameObject signalObjectGreen;
    public GameObject cage;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip finalizationSound;
    private bool isFinalizedSound = false;
    
    void Update()
    {
        /*
        if (Dest.IsCorrectlyPlaced())
        {
            signalObjectRed.SetActive(false);
            signalObjectYellow.SetActive(true);
        }
        */
        
        if (Dest.IsCorrectlyPlaced() && Dest2.IsCorrectlyPlaced() && !isFinalizedSound)
        {
            signalObjectYellow.SetActive(false);
            signalObjectGreen.SetActive(true);
            StartCoroutine(DestroyAfterDelay());
            isFinalizedSound = true;
        }
        else if ((Dest.IsCorrectlyPlaced() && !Dest2.IsCorrectlyPlaced() || !Dest.IsCorrectlyPlaced() && Dest2.IsCorrectlyPlaced()) && !isFinalizedSound)
        {
            signalObjectRed.SetActive(false);
            signalObjectYellow.SetActive(true);
        }
        else if (signalObjectYellow.activeSelf && !isFinalizedSound)
        {
            signalObjectYellow.SetActive(false);
            signalObjectRed.SetActive(true);
        }
        else { }
    }

    private IEnumerator DestroyAfterDelay()
    {
        sfxAcess.Audio(finalizationSound);
        yield return new WaitForSeconds(finalizationSound.length);
        Destroy(cage.gameObject);
        this.gameObject.SetActive(false);
    }
}

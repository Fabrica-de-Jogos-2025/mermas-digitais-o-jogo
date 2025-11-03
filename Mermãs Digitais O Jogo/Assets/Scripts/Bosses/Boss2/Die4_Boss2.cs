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
    
    void Update()
    {
        if (Dest.IsCorrectlyPlaced())
        {
            signalObjectRed.SetActive(false);
            signalObjectYellow.SetActive(true);
        }
        
        if (Dest.IsCorrectlyPlaced() && Dest2.IsCorrectlyPlaced())
        {
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        sfxAcess.Audio(finalizationSound);
        yield return new WaitForSeconds(finalizationSound.length);        
        signalObjectYellow.SetActive(false);
        signalObjectGreen.SetActive(true);
        Destroy(cage.gameObject);
        this.gameObject.SetActive(false);
    }
}

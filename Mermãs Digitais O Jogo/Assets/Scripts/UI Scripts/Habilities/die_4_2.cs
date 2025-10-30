using UnityEngine;

public class die_4_2 : MonoBehaviour
{
    private PlayerMovement player;
    public Hability4 Dest;
    public Hability4_1 Dest2;
    public GameObject signalObjectRed;
    public GameObject signalObjectYellow;
    public GameObject signalObjectGreen;
    public GameObject cage;
    // public float destroyInSec = 3f;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hability;
    private bool hasPlayed = false;
    // Update is called once per frame

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {

        if (Dest.IsCorrectlyPlaced() && Dest2.IsCorrectlyPlaced() && !hasPlayed)
        {
            hasPlayed = true;
            signalObjectYellow.SetActive(false);
            signalObjectGreen.SetActive(true);
            Destroy(cage.gameObject);
            StartCoroutine(DestroyAfterDelay());
        }
        else if (Dest.IsCorrectlyPlaced() && !Dest2.IsCorrectlyPlaced() || !Dest.IsCorrectlyPlaced() && Dest2.IsCorrectlyPlaced())
        {
            signalObjectRed.SetActive(false);
            signalObjectYellow.SetActive(true);
        }
        else if (signalObjectYellow.activeSelf)
        {
            signalObjectYellow.SetActive(false);
            signalObjectRed.SetActive(true);
        }
        else { }
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        sfxAcess.Audio(hability);
        yield return new WaitForSeconds(hability.length);
        Destroy(this.gameObject);
        player.IsFrozen = false;
    }
}

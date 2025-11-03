using System.Collections;
using UnityEngine;

public class die_3 : MonoBehaviour
{

    public Hability3 Dest;
    [SerializeField] private GameObject screenHability;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hability;
    private bool hasPlayed = false;

    public GameObject habilityCard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Dest.puzzleSolved && !hasPlayed)
        {
            hasPlayed = true;
            screenHability.SetActive(false);
            StartCoroutine(HandlePuzzleSolved());
        }
    }

    private IEnumerator HandlePuzzleSolved()
    {
        // screenHability.SetActive(false);
        sfxAcess.Audio(hability); // Toca o som primeiro

        yield return new WaitForSeconds(hability.length);

        if (Dest.cardAppear)
        {
            habilityCard.SetActive(true);
        }

        Destroy(this.gameObject);
    }
}

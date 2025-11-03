using UnityEngine;
using System.Collections;

public class Die3_v2_Boss2 : MonoBehaviour
{
    public Hability3_Boss2_2 Dest;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip finalizationSound;

    private bool isPlaying = false;

    void Update()
    {
        if (Dest.puzzleSolved && !isPlaying)
        {
            isPlaying = true;
            StartCoroutine(PlayAndDisable());
        }
    }

    private IEnumerator PlayAndDisable()
    {
        sfxAcess.Audio(finalizationSound);
        yield return new WaitForSeconds(finalizationSound.length);
        this.gameObject.SetActive(false);
    }
}

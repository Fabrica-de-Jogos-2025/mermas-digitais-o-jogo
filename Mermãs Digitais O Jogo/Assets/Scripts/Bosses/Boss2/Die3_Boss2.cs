using UnityEngine;
using System.Collections;

public class Die3_Boss2 : MonoBehaviour
{
    public Hability3_Boss2 Dest;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip finalizationSound;

    private bool pemissao = false;

    void Update()
    {
        if (Dest.puzzleSolved && !pemissao)
        {
            pemissao = true;
            StartCoroutine(desabilitarAposFinalizarAudio());
        }
    }

    private IEnumerator desabilitarAposFinalizarAudio()
    {
        sfxAcess.Audio(finalizationSound);
        yield return new WaitForSeconds(finalizationSound.length);
        this.gameObject.SetActive(false);
    }
}

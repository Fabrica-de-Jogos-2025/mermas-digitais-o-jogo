using UnityEngine;

public class GameplayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource loopSource;
    [SerializeField] private AudioSource sfxSource;
    public void Audio(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void LoopAudio(AudioClip clip)
    {
        if (clip == null || loopSource == null) return;
        if (loopSource.clip == clip && loopSource.isPlaying) return;
        loopSource.clip = clip;
        loopSource.loop = true;
        loopSource.Play();
    }

    public void StopAudio()
    {
        if (loopSource != null && loopSource.isPlaying)
            loopSource.Stop();
    }
}

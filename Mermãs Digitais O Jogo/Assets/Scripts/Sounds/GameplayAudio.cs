using UnityEngine;

public class GameplayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void Audio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void LoopAudio(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;
        if (audioSource.clip == clip && audioSource.isPlaying) return;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class Hability6 : MonoBehaviour, IPointerClickHandler
{
    public VideoPlayer video;
    public GameObject uiCanvas;
    public GameObject ship;
    public GameObject hitbox;
    [SerializeField] private Image rightAnswer;
    [SerializeField] private Image wrongAnswer1;
    [SerializeField] private Image wrongAnswer2;
    public GameObject videoPanel;
    public RawImage videoDisplay;
    public RenderTexture renderTexture;
    public bool UsoDaUltimaHabilidade = false;
    private bool verificationclick = false;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hability;
    // private bool hasPlayed = false;

    private void Start()
    {
        if (video == null)
        {
            video = FindObjectOfType<VideoPlayer>();
        }

        if (video == null)
        {
            Debug.LogError("Nenhum VideoPlayer encontrado na cena!");
            return;
        }

        if (renderTexture != null)
        {
            video.targetTexture = renderTexture;
            videoDisplay.texture = renderTexture;
        }

        videoPanel.SetActive(false);
        video.loopPointReached += OnVideoEnd;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (video != null && uiCanvas != null)
        {
            if (wrongAnswer1 != null)
            {
                Destroy(wrongAnswer1.gameObject);
            }

            if (wrongAnswer2 != null)
            {
                Destroy(wrongAnswer2.gameObject);
            }

            /*Destroy(rightAnswer.gameObject, hability.length);
            videoPanel.SetActive(true);
            videoDisplay.gameObject.SetActive(true);
            video.Play();*/

            StartCoroutine(VideoPlayerController());
        }

        if (ship != null)
        {
            verificationclick = true;
            UsoDaUltimaHabilidade = true;
            //Destroy(hitbox.gameObject);
            Destroy(ship);
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (uiCanvas != null && verificationclick)
        {
            uiCanvas.SetActive(false);
        }
        videoPanel.SetActive(false);
        videoDisplay.gameObject.SetActive(false);
    }

    private IEnumerator VideoPlayerController()
    {
        sfxAcess.Audio(hability);
        Destroy(rightAnswer.gameObject, hability.length);
        
        yield return new WaitForSeconds(0f);
        
        videoPanel.SetActive(true);
        videoDisplay.gameObject.SetActive(true);
        video.Play();
    }
}

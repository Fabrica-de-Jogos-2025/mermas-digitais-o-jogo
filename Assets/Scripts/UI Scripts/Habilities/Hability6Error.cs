using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class Hability6Error : MonoBehaviour, IPointerClickHandler
{
    public VideoPlayer video;
    public GameObject uiCanvas;
    [SerializeField] private Image wrongAnswer;
    public GameObject videoPanel;
    public RawImage videoDisplay;
    public RenderTexture renderTexture;

    private void Start()
    {
        if (video == null)
        {
            video = FindObjectOfType<VideoPlayer>();
        }

        if (video == null)
        {
            uiCanvas.SetActive(true);
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
            videoPanel.SetActive(true);
            videoDisplay.gameObject.SetActive(true);
            video.Play();
            Destroy(wrongAnswer.gameObject);
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        uiCanvas.SetActive(true);
        /*Destroy(videoPanel.gameObject);
        Destroy(videoDisplay.gameObject);*/
        videoPanel.SetActive(false);
        videoDisplay.gameObject.SetActive(false);
    }
}

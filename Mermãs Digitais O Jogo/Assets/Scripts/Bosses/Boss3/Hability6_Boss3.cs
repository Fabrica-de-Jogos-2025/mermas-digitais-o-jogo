using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class Hability6_Boss3 : MonoBehaviour, IPointerClickHandler
{
    public VideoPlayer video;
    public GameObject uiCanvas;
    public GameObject ship;
    [SerializeField] private Image rightAnswer;
    [SerializeField] private Image wrongAnswer1;
    [SerializeField] private Image wrongAnswer2;
    public GameObject videoPanel; 
    public RawImage videoDisplay; 
    public RenderTexture renderTexture;
    public bool UsoDaUltimaHabilidade = false;
    public Boss3_Attack Z;
    private PlayerMovement player;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip finalizationSound;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>(); 

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

            
            StartCoroutine(desabilitarAposFinalizarAudio());
        }

        if (ship != null)
        {
            UsoDaUltimaHabilidade = true;
            Destroy(ship);
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (uiCanvas != null)
        {
            uiCanvas.SetActive(false);
        }
        videoPanel.SetActive(false);
        videoDisplay.gameObject.SetActive(false);
        Z.z = true;
        player.IsFrozen = false;
    }
    
    private IEnumerator desabilitarAposFinalizarAudio()
    {
        sfxAcess.Audio(finalizationSound);
        yield return new WaitForSeconds(finalizationSound.length);
        
        Destroy(rightAnswer.gameObject);
        videoPanel.SetActive(true); 
        videoDisplay.gameObject.SetActive(true);
        video.Play();
        video.loopPointReached += OnVideoEnd;
    }
}

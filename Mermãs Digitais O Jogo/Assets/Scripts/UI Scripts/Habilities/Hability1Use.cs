using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hability1Use : MonoBehaviour
{
    [SerializeField] private string booleanOperation;
    public Sprite[] images;
    [SerializeField] private Sprite[] correctImages;
    private Image imageButton;
    [SerializeField] private Image[] buttons;
    private Hability1 habilityScreen;
    //public Hability1 habilityScreen;   
    private int currentIndex = 0;
    private bool puzzleSolved = false;
    private Padlock padlock;
    public bool PuzzleSolved { get => puzzleSolved; set => puzzleSolved = value; }
    public PlayerMovement PM;
    public bool destroy = false;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hability;
    [SerializeField] private AudioClip click;

    void Start()
    {
        padlock = FindAnyObjectByType<Padlock>();
        habilityScreen = FindAnyObjectByType<Hability1>();
        imageButton = GetComponent<Image>();
        imageButton.sprite = images[currentIndex];
    }

    public void SwitchImage()
    {
        sfxAcess.Audio(click);
        currentIndex = (currentIndex + 1) % images.Length; 
        imageButton.sprite = images[currentIndex];

        VerifyPuzzle();
    }

    public void VerifyPuzzle()
    {
        
        if (booleanOperation == "Negação")
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].sprite != correctImages[i])
                {
                    return;
                }
            }
            puzzleSolved = true;

            if (puzzleSolved)
            {
                /*sfxAcess.Audio(hability);
                padlock.Anim.SetInteger("transition", 0);
                habilityScreen.HabilityScreen.SetActive(false);
                //Destroy(habilityScreen.gameObject);
                //Destroy(padlock.gameObject);
                //padlock.gameObject.SetActive(false);
                padlock.i = true;
                destroy = true;*/
                StartCoroutine(HandlePuzzleSolved());
            }
        }
    }

    private IEnumerator HandlePuzzleSolved()
    {
        sfxAcess.Audio(hability); // Toca o som primeiro
        padlock.Anim.SetInteger("transition", 0);
        padlock.i = true;
        destroy = true;

        // Espera o som terminar antes de desativar a tela
        yield return new WaitForSeconds(hability.length);

        // Agora pode desativar a tela sem cortar o áudio
        habilityScreen.HabilityScreen.SetActive(false);
    }
}


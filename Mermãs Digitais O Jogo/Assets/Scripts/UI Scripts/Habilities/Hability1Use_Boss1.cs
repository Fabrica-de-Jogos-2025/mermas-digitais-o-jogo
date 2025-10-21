using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hability1Use_Boss1 : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private string booleanOperation;
    public Sprite[] images;
    [SerializeField] private Sprite[] correctImages;
    private Image imageButton;
    [SerializeField] private Image[] buttons;
    private Hability1_1 habilityScreen;
    private int currentIndex = 0;
    private bool puzzleSolved = false;
    private Padlock padlock;
    public bool PuzzleSolved { get => puzzleSolved; set => puzzleSolved = value; }
    public bool destroy = false;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hability;
    [SerializeField] private AudioClip click;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        habilityScreen = FindAnyObjectByType<Hability1_1>();
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

        if (booleanOperation == "Disjunção")
        {
            for (int i = 0; i < 2; i++)
            {
                if (buttons[i].sprite != correctImages[i])
                {
                    for (int j = 2; j < 4; j++)
                    {
                        if (buttons[j].sprite != correctImages[j])
                        {
                            return;
                        }
                    }
                }
            }
            puzzleSolved = true;

            if (puzzleSolved)
            {
                // habilityScreen.pausarJogador = false;
                //player.IsFrozen = false;
                //habilityScreen.HabilityScreen.SetActive(false);
                //destroy = true;
                StartCoroutine(HandlePuzzleSolved());
            }
        }
    }
    
    private IEnumerator HandlePuzzleSolved()
    {
        sfxAcess.Audio(hability); // Toca o som primeiro
        player.IsFrozen = false;
        destroy = true;

        // Espera o som terminar antes de desativar a tela
        yield return new WaitForSeconds(hability.length);

        // Agora pode desativar a tela sem cortar o áudio
        habilityScreen.HabilityScreen.SetActive(false);
    }
}
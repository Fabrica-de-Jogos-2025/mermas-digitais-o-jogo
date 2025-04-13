using UnityEngine;
using UnityEngine.UI;

public class Hability1Use_1 : MonoBehaviour
{
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

    void Start()
    {
        habilityScreen = FindAnyObjectByType<Hability1_1>();
        imageButton = GetComponent<Image>();
        imageButton.sprite = images[currentIndex];
    }

    public void SwitchImage()
    {

        currentIndex = (currentIndex + 1) % images.Length; 
        imageButton.sprite = images[currentIndex];

        VerifyPuzzle();
    }

    public void VerifyPuzzle()
    {
        
        if (booleanOperation == "Conjunção")
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
                habilityScreen.pausarJogador = false;
                habilityScreen.HabilityScreen.SetActive(false);
                destroy = true;
            }
        }
    }
}
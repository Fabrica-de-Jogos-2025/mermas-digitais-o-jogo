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
    private int count = 0;

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
                destroy = true;
                habilityScreen.HabilityScreen.SetActive(false);
            }
        }

        else if (booleanOperation == "Implicação")
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].sprite == correctImages[i])
                {
                    count++;
                    if (count == buttons.Length)
                    {
                        count = 0;
                        return;
                    }
                }
            }
            
            count = 0;

            if (buttons[buttons.Length - 1].sprite == correctImages[buttons.Length - 1])
            {
                puzzleSolved = true;

                if (puzzleSolved)
                {
                    habilityScreen.pausarJogador = false;
                    destroy = true;
                    habilityScreen.HabilityScreen.SetActive(false);
                }
            }
        }
    }
}
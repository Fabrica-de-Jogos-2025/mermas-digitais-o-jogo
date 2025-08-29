using TMPro;
using UnityEngine;

public class Hability3_Boss2_2 : MonoBehaviour
{
    [SerializeField] private TMP_InputField numberInput;
    [SerializeField] private TMP_InputField numberInput2;
    public bool puzzleSolved = false;
    private bool i = false;
    private bool j = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numberInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NumberCaption() {
        if (int.TryParse(numberInput.text, out int number))
        {
            if (number == 3)
            {
                i = true;
            }
            else
            {
                i = false;
            }
        } 

        if (int.TryParse(numberInput2.text, out int secondNumber))
        {
            if (secondNumber == 2)
            {
                j = true;
            }
            else
            {
                j = false;
            }
        }

        if(i && j)
        {    
            puzzleSolved = true;
        }
    }
}

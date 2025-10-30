using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Hability3 : MonoBehaviour
{
    [SerializeField] private TMP_InputField numberInput;
    [SerializeField] private TMP_InputField numberInput2;
    public bool puzzleSolved = false;
    public bool cardAppear = false;
    private bool i = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numberInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
    }

    public void NumberCaption() {
        if (int.TryParse(numberInput.text, out int number))
        {
            if (number == 0)
            {
                i = true;
                puzzleSolved = true;
            }
        } 

        if (int.TryParse(numberInput2.text, out int secondNumber))
        {
            if ((secondNumber == 0) && i)
            {
                cardAppear = true;
            } 
        }
    }
}

using TMPro;
using UnityEngine;

public class Hability3 : MonoBehaviour
{
    [SerializeField] private TMP_InputField numberInput;
    [SerializeField] private TMP_InputField numberInput2;
    public bool puzzleSolved = false;
    private bool cardAppear = false;
    
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
            if (number == 0)
            {
                puzzleSolved = true;
            }
        } 

        if (int.TryParse(numberInput.text, out int secondNumber))
        {
            if (secondNumber == 0)
            {
                cardAppear = true;
            } 
        }
    }
}

using TMPro;
using UnityEngine;

public class Hability3_Boss2 : MonoBehaviour
{
    [SerializeField] private TMP_InputField numberInput;
    public bool puzzleSolved = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numberInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NumberCaption()
    {
        if (int.TryParse(numberInput.text, out int number))
        {
            if (number == 1)
            {
                puzzleSolved = true;
            }
        }
    }
}

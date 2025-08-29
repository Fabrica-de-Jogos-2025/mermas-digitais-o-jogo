using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Teste_Hability5 : MonoBehaviour
{
    [SerializeField] private TMP_InputField numberInput;
    public bool puzzleSolved = false;
    
    void Start()
    {
        numberInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
    }

    public void NumberCaption() {
        if (int.TryParse(numberInput.text, out int number))
        {
            if ((number == 0) && Teste2_Hability5.allDraggableObjects.TrueForAll(obj => obj.IsCorrectlyPlaced()))
            {
                puzzleSolved = true;
            }
            else if (number != 0)
            {
                puzzleSolved = false;
            }
        }
    }
}


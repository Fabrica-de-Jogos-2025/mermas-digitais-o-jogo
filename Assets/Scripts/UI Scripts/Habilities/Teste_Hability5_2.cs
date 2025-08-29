using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Teste_Hability5_2 : MonoBehaviour
{
    //[SerializeField] private TMP_InputField numberInput;
    public Teste2_Hability5 Close;
    public bool puzzleSolved = false;
    
    void Start()
    {
        //numberInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
    }

    void update()
    {
        Verification();
    }

    public void Verification() 
    {
        if (Close.close)
        {
            puzzleSolved = true;
        }
        else
        {
            puzzleSolved = false;
        }
    }

    /*public void NumberCaption() {
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
    }*/
}


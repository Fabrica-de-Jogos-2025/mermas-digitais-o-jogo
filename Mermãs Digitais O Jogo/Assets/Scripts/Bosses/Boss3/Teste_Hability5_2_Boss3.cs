using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Teste_Hability5_2_Boss3 : MonoBehaviour
{
    public Teste2_Hability5 Close;
    public bool puzzleSolved = false;

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
}


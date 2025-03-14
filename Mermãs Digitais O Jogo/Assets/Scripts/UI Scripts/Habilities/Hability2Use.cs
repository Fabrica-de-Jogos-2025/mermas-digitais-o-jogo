using System.Linq;
using UnityEngine;

public class Hability2Use : MonoBehaviour
{
    private GameObject uiContainer;

    public static void CheckAllPositions()
    {
        bool allCorrect = Hability2.allDraggableObjects.All(obj => obj.IsCorrectlyPlaced());

        if (allCorrect)
        {
            Debug.Log("Todas as imagens estão na posição correta!");

            // Encontrar o objeto com Hability2Use e esconder a UI
            Hability2Use instance = FindObjectOfType<Hability2Use>();
            if (instance != null && instance.uiContainer != null)
            {
                instance.uiContainer.SetActive(false);
            }
        }
    }
}

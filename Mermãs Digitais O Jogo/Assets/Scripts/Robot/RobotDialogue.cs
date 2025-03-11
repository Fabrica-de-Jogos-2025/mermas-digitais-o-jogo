using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RobotDialogue : MonoBehaviour
{
    [Header("Configurações de Diálogo")]
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Image imageRobot;
    public Sprite spriteRobot;
    public float charactersPerSecond = 30f;

    private string[] currentDialogue;
    private int dialogueIndex;
    private bool dialogueActive;
    private bool isTyping;
    private Coroutine currentTypingCoroutine;
    private PlayerMovement currentPlayer;

    void Start()
    {
        dialoguePanel.SetActive(false);
        dialogueIndex = 0;
        
        if(imageRobot != null && spriteRobot != null)
        {
            imageRobot.sprite = spriteRobot;
        }
    }

    void Update()
    {
        if(dialogueActive && Input.GetKeyDown(KeyCode.X))
        {
            if(isTyping)
            {
                if(currentTypingCoroutine != null)
                {
                    StopCoroutine(currentTypingCoroutine);
                }
                dialogueText.text = currentDialogue[dialogueIndex];
                isTyping = false;
            }
            else
            {
                NextMessage();
            }
        }
    }

    public void StartDialogue(string[] dialogue, PlayerMovement player)
    {
        if(!dialogueActive)
        {
            currentDialogue = dialogue;
            dialogueActive = true;
            dialoguePanel.SetActive(true);
            dialogueIndex = 0;
            
            // Resetar estado de digitação
            if(currentTypingCoroutine != null)
            {
                StopCoroutine(currentTypingCoroutine);
            }
            
            ShowMessage(dialogueIndex);

            currentPlayer = player;
            if(currentPlayer != null)
            {
                currentPlayer.FreezePlayer(true);
            }
        }
    }

    void NextMessage()
    {
        dialogueIndex++;
        
        if(dialogueIndex < currentDialogue.Length)
        {
            ShowMessage(dialogueIndex);
        }
        else
        {
            EndDialogue();
        }
    }

    void ShowMessage(int index)
    {
        if(currentTypingCoroutine != null)
        {
            StopCoroutine(currentTypingCoroutine);
        }
        currentTypingCoroutine = StartCoroutine(TypeText(currentDialogue[index]));
    }

    IEnumerator TypeText(string message)
    {
        isTyping = true;
        dialogueText.text = "";
        
        float delay = 1.5f / charactersPerSecond;
        
        foreach(char character in message)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(delay);
        }
        
        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueActive = false;
        dialogueIndex = 0;
        isTyping = false;
        
        if(currentTypingCoroutine != null)
        {
            StopCoroutine(currentTypingCoroutine);
            currentTypingCoroutine = null;
        }

        if(currentPlayer != null)
        {
            currentPlayer.FreezePlayer(false);
            currentPlayer = null;
        }
    }
}
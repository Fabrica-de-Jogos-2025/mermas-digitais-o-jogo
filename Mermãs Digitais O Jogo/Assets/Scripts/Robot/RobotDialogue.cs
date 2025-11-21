using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;

public class RobotDialogue : MonoBehaviour
{
    [System.Serializable]
    public class PalavraColorida
    {
        public string palavra;
        public Color cor;
    }

    [Header("Configurações de Diálogo")]
    public GameObject uiPlayer;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text characterNameText;
    public Image imageRobot;
    public Sprite spriteRobot;
    public float charactersPerSecond = 30f;

    [Header("Palavras em destaque")]
    public List<PalavraColorida> palavrasColoridas = new List<PalavraColorida>();
    public Action OnDialogueEnd;

    private string[] currentDialogue;
    [SerializeField] private AudioClip dialogueSFX;
    [SerializeField] private GameplayAudio sfxAcess;
    private string currentProcessedMessage;
    private string characterName;
    public int dialogueIndex;
    private bool dialogueActive;
    private bool isTyping;
    private Coroutine currentTypingCoroutine;
    private PlayerMovement currentPlayer;
    private RobotMovement robo;
    public bool permissionToProceed;
    public bool permission;

    public InputController controls;
    private InputAction dialogProceed;
    private InputAction dialogSkip;
    public bool dialogoExecutadoNoBoss = false;

    public bool DialogueActive { get => dialogueActive; set => dialogueActive = value; }

    private void Awake()
    {
        controls = new InputController();
    }

    void OnEnable()
    {
        dialogProceed = controls.Player.ProceedDialogue;
        dialogProceed.Enable();

        dialogSkip = controls.Player.JumpDialogue;
        dialogSkip.Enable();
    }

    void OnDisable()
    {
        dialogProceed.Disable();
        dialogSkip.Disable();
    }

    void Start()
    {
        uiPlayer = GameObject.Find("CanvasHUD");
        robo = FindAnyObjectByType<RobotMovement>();

        GameObject dialogueParent = GameObject.Find("Dialogue");

        if (dialogueParent != null)
        {
            Transform[] allChildren = dialogueParent.GetComponentsInChildren<Transform>(true);

            foreach (Transform child in allChildren)
            {
                switch (child.name)
                {
                    case "DialoguePanel":
                        dialoguePanel = child.gameObject;
                        break;
                    case "DialogueText":
                        dialogueText = child.GetComponent<Text>();
                        break;
                    case "CharacterName":
                        characterNameText = child.GetComponent<Text>();
                        break;
                    case "ImageRobot":
                        imageRobot = child.GetComponent<Image>();
                        break;
                }
            }
        }

        uiPlayer.SetActive(true);
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        dialogueIndex = 0;

        if (imageRobot != null && spriteRobot != null && name != null)
        {
            imageRobot.sprite = spriteRobot;
            characterNameText.text = characterName;
        }

        if (dialogueText != null)
        {
            dialogueText.supportRichText = true;
        }
        permissionToProceed = true;
        permission = true;
    }

    void Update()
    {
        if (!dialogueActive)
            return;

        bool skipPressed = Input.GetKeyDown(KeyCode.Y) || dialogSkip.WasPerformedThisFrame();
        if (skipPressed && permission)
        {
            EndDialogue();
            return;
        }

        bool proceedPressed = Input.GetKeyDown(KeyCode.X) || dialogProceed.WasPerformedThisFrame();
        if (proceedPressed)
        {
            if (isTyping)
            {
                StopTypingInstantly();
            }
            else if (dialogoExecutadoNoBoss)
            {
                dialogoExecutadoNoBoss = false;
                EndDialogue();
            }
            else if (permissionToProceed)
            {
                NextMessage();
            }
        }
    }


    public void StartDialogue(string[] dialogue, PlayerMovement player)
    {
        if (robo != null)
            robo.SetDialogueState(true);

        if (!dialogueActive)
        {
            currentDialogue = dialogue;
            dialogueActive = true;
            uiPlayer.SetActive(false);
            dialoguePanel.SetActive(true);
            dialogueIndex = 0;

            // Resetar estado de digitação
            if (currentTypingCoroutine != null)
            {
                StopCoroutine(currentTypingCoroutine);
            }

            ShowMessage(dialogueIndex);

            currentPlayer = player;
            if (currentPlayer != null)
            {
                currentPlayer.FreezePlayer(true);
            }
        }
    }

    public void NextMessage()
    {
        dialogueIndex++;

        if (dialogueIndex < currentDialogue.Length)
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
        if (currentTypingCoroutine != null)
        {
            StopCoroutine(currentTypingCoroutine);
            currentTypingCoroutine = null;
        }
        /*string message = ApplyColors(currentDialogue[index]);
        currentTypingCoroutine = StartCoroutine(TypeText(currentDialogue[index]));*/

        // processa AQUI a mensagem (com as tags <color>)
        currentProcessedMessage = ApplyColors(currentDialogue[index]);

        // inicia a digitação usando a mensagem processada
        currentTypingCoroutine = StartCoroutine(TypeText(currentProcessedMessage));
    }

    IEnumerator TypeText(string message)
    {
        isTyping = true;
        dialogueText.text = "";

        if (dialogueActive && dialogueSFX != null && sfxAcess != null)
        {
            // Garante que não toque várias vezes
            sfxAcess.StopAudio();
            sfxAcess.LoopAudio(dialogueSFX); // <-- inicia o loop
        }

        float delay = 0.2f / charactersPerSecond;

        int i = 0;
        while (i < message.Length)
        {
            char c = message[i];

            if (c == '<')
            {
                // é uma tag — copia até o '>' imediatamente, sem delay
                int close = message.IndexOf('>', i);
                if (close == -1)
                {
                    // tag malformada: apenas adiciona o char atual
                    dialogueText.text += message[i];
                    i++;
                }
                else
                {
                    dialogueText.text += message.Substring(i, close - i + 1);
                    i = close + 1;
                }
            }
            else
            {
                // caractere normal: escreve e espera
                dialogueText.text += c;
                i++;
                yield return new WaitForSeconds(delay);
            }
        }

        if (sfxAcess != null)
            sfxAcess.StopAudio();

        isTyping = false;
        currentTypingCoroutine = null;
    }

    private void StopTypingInstantly()
    {
        if (currentTypingCoroutine != null)
        {
            StopCoroutine(currentTypingCoroutine);
            currentTypingCoroutine = null;
        }

        dialogueText.text = currentProcessedMessage;
        isTyping = false;

        if (sfxAcess != null)
            sfxAcess.StopAudio();
    }

    string ApplyColors(string textoOriginal)
    {
        string textoFormatado = textoOriginal;

        foreach (var item in palavrasColoridas)
        {
            if (!string.IsNullOrEmpty(item.palavra))
            {
                string corHex = UnityEngine.ColorUtility.ToHtmlStringRGB(item.cor);
                textoFormatado = textoFormatado.Replace(
                    item.palavra,
                    $"<color=#{corHex}>{item.palavra}</color>"
                );
            }
        }

        return textoFormatado;
    }

    public void EndDialogue()
    {
        uiPlayer.SetActive(true);
        dialoguePanel.SetActive(false);
        dialogueActive = false;
        dialogueIndex = 0;
        isTyping = false;

        permission = true;

        if (currentTypingCoroutine != null)
        {
            StopCoroutine(currentTypingCoroutine);
            currentTypingCoroutine = null;
        }

        if (currentPlayer != null)
        {
            currentPlayer.FreezePlayer(false);
            currentPlayer = null;
        }

        OnDialogueEnd?.Invoke();
        OnDialogueEnd = null;
        sfxAcess.StopAudio();

        if (robo != null)
            robo.SetDialogueState(false);
    }
}
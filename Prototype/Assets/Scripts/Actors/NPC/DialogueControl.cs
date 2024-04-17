using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObject; //janela do diálogo
    public Image profileSprite; // imagem do NPC
    public Text speechText; // texto da fala
    public Text actorNameText; // nome do NPC

    [Header("Settings")]
    public float typingSpeed; // velocidade da fala

    // Variáveis de controle.
    [HideInInspector] public bool isShowing; // se a janela está visível
    private int index; // index das sentenças
    private string[] sentences;
    private Player player;

    public static DialogueControl instance;

    // método chamado antes de todos os Start()
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            NextSentence();
        }
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // pula para a próxima frase/fala
    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            { // quando terminam os textos
                speechText.text = "";
                index = 0;
                dialogueObject.SetActive(false);
                sentences = null;
                isShowing = false;
                player.isPaused = false;
            }
        }
    }
    // chamar a fala do NPC
    public void Speech(string[] txt)
    {
        if (!isShowing)
        {
            dialogueObject.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
            player.isPaused = true;
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;


public class hab_icon : MonoBehaviour
{
    public GameObject habilityUITutorial;
    [SerializeField] private GameObject wind;

    private AudioSource windAudio;
    private bool playerInside = false;

    private void Start()
    {
        // Garante que o objeto 'wind' tenha um AudioSource
        if (wind != null)
            windAudio = wind.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Só reage à tecla H enquanto o jogador está dentro do colisor
        if (playerInside && Input.GetKeyDown(KeyCode.H))
        {
            habilityUITutorial.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            if (windAudio != null && wind.CompareTag("Hability4"))
                windAudio.volume = 1f; // ativa o som

            habilityUITutorial.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (windAudio != null && wind.CompareTag("Hability4"))
                windAudio.volume = 0f; // desativa o som

            habilityUITutorial.SetActive(false);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wind = GetComponent<GameObject>();
            if (wind != null && wind.CompareTag("Hability4"))
            {
                wind.GetComponent<AudioSource>().volume = 1;
            }
            habilityUITutorial.SetActive(true);

            if (Input.GetKeyDown(KeyCode.H))
            {
                habilityUITutorial.SetActive(false);
                //Destroy(habilityUITutorial.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            habilityUITutorial.SetActive(false);
        }
    }*/
}

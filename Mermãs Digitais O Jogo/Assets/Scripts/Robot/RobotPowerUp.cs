using UnityEngine;

public class RobotPowerUp : MonoBehaviour
{
    //[SerializeField] private bool hasPowerUp = false;
    private PlayerMovement player;
    [SerializeField] private GameObject powerUpIcon;
    //private RobotMovement robot;

    //public bool HasPowerUp { get => hasPowerUp; set => hasPowerUp = value; }

    [SerializeField] private bool isTutorialPowerUp;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip powerupClip;
    public RobotMovement robotPowered;

    private bool collected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        //robot = FindObjectOfType<RobotMovement>();
        robotPowered = FindAnyObjectByType<RobotMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.CompareTag("Player"))
        {
            collected = true;
            powerUpIcon.SetActive(true);
            sfxAcess.Audio(powerupClip); // toca o som primeiro
            robotPowered.HasPowerUp = true;

            if (!isTutorialPowerUp)
            {
                // destrói o PowerUp depois de um pequeno atraso
                Destroy(gameObject, powerupClip.length);
            }
        }
    }
}

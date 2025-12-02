using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private int playerLife;
    [SerializeField] private int cards;
    [SerializeField] private GameObject canvas;

    // private Checkpoint checkpoint;
    private string cenaAtual;
    // private TryAgainScreen yesButton;
    private RobotMovement robot;


    public int PlayerLife { get => playerLife; set => playerLife = value; }
    public Image[] Hearts { get => hearts; set => hearts = value; }
    public int Cards { get => cards; set => cards = value; }
    public string CenaAtual { get => cenaAtual; set => cenaAtual = value; }
    public RobotMovement Robot { get => robot; set => robot = value; }
    public GameObject Canvas { get => canvas; set => canvas = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.Find("CanvasHUD");
        GameObject heartsParent = GameObject.Find("Hearts");

        if (heartsParent != null)
        {
            // Pega todos os Image filhos (inclusive os desativados se quiser)
            hearts = heartsParent.GetComponentsInChildren<Image>(true);
        }

        playerLife = hearts.Length;
        // checkpoint = FindFirstObjectByType<Checkpoint>();
        cenaAtual = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastScene", cenaAtual);

        // yesButton = GameObject.Find("Canvas").GetComponentInChildren<TryAgainScreen>();
        robot = GameObject.Find("Robot(Clone)").GetComponent<RobotMovement>();
    }
    public void Die()
    {
        if (PlayerLife <= 0)
        {
            GetComponent<PlayerMovement>().FreezePlayer(true);

            // salva estado do checkpoint (se tiver)
            if (GetComponent<PlayerMovement>().LastCheckpointPosition != Vector3.zero)
            {
                PlayerPrefs.SetInt("CheckpointAtivo", 1);
                PlayerPrefs.SetFloat("CheckpointX", GetComponent<PlayerMovement>().LastCheckpointPosition.x);
                PlayerPrefs.SetFloat("CheckpointY", GetComponent<PlayerMovement>().LastCheckpointPosition.y);
                PlayerPrefs.SetFloat("CheckpointZ", GetComponent<PlayerMovement>().LastCheckpointPosition.z);
            }
            else
            {
                PlayerPrefs.SetInt("CheckpointAtivo", 0);
            }
            PlayerPrefs.Save();

            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
            if (this.gameObject != null) Destroy(this.gameObject);
            if (robot != null) Destroy(robot.gameObject);

            // desativa HUD
            canvas.SetActive(false);
            SceneManager.LoadScene("Morte_Falha");
        }
    }
}

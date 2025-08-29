using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private int playerLife;
    [SerializeField] private int cards;

    public int PlayerLife { get => playerLife; set => playerLife = value; }
    public Image[] Hearts { get => hearts; set => hearts = value; }
    public int Cards { get => cards; set => cards = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLife = hearts.Length;
    }
    public void Die()
    {
        if (PlayerLife <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Tutorial");
        }
    }
}

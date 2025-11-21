using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController Instance;
    private int totalCoins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
            totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); // Carrega moedas salvas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }

    public int GetCoins()
    {
        return totalCoins;
    }

    public void SpendCoins(int amount)
    {
        totalCoins -= amount;
        if (totalCoins < 0) totalCoins = 0;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }
}

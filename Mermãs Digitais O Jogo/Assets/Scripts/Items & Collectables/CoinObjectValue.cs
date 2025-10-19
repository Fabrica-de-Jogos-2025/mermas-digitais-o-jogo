using TMPro;
using UnityEngine;

public class CoinObjectValue : MonoBehaviour
{
    public int coinCount;
    public static CoinObjectValue instance;
    // private TextMeshProUGUI CoinsNumber;
    private CoinManager CoinManager;
    private TextMeshProUGUI CoinsQuantity;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        CoinManager = GameObject.Find("Coins").GetComponent<CoinManager>();
        coinCount = CoinManager.coinCount; // PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinUI();
        // CoinsNumber = GameObject.Find("CoinsNumber").GetComponent<TextMeshProUGUI>();
    }

    /*public void AddCoin()
    {
        coinCount++;
        CoinsNumber.text = coinCount.ToString();
        PlayerPrefs.SetInt("TotalCoins", coinCount);
        PlayerPrefs.Save();
        UpdateCoinUI();
    }*/

    public void UpdateCoinUI()
    {
        // Só tenta atualizar se o TextMeshPro existir na cena atual
        // GameObject coinTextObj = GameObject.Find("CoinsNumber");
        GameObject coinQuantity = GameObject.Find("CoinsQuantity");
        /*if (coinTextObj != null)
        {
            CoinsNumber = coinTextObj.GetComponent<TextMeshProUGUI>();
            CoinsNumber.text = coinCount.ToString();
        }*/

        if (coinQuantity != null)
        {
            CoinsQuantity = coinQuantity.GetComponent<TextMeshProUGUI>();
            CoinsQuantity.text = coinCount.ToString();
        }
    }
}

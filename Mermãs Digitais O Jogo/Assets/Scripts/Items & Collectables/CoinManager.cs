using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    private TextMeshProUGUI CoinsNumber;

    private void Start()
    {
        CoinsNumber = GameObject.Find("CoinsNumber").GetComponent<TextMeshProUGUI>();
    }

    public void AddCoin()
    {
        coinCount++;
        CoinsNumber.text = coinCount.ToString();
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public TextMeshProUGUI CoinsNumber;

    public void AddCoin()
    {
        coinCount++;
        CoinsNumber.text = coinCount.ToString();
    }
}

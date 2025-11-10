using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreSystem : MonoBehaviour
{
    [Header("Referências da UI")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI[] itemTexts; // Os textos dos 7 itens
    [SerializeField] private Button[] itemButtons;
    [SerializeField] private GameObject[] coinIcons;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip moneyClip;
    [SerializeField] private AudioClip click;

    private int itemCost = 80;
    private int totalCoins;

    private void Start()
    {
        UpdateUI();

        for (int i = 0; i < itemButtons.Length; i++)
        {
            int index = i; // Necessário para capturar o valor correto no loop
            itemButtons[i].onClick.AddListener(() => OnItemClick(index));
        }
    }

    private void UpdateUI()
    {
        totalCoins = CoinController.Instance.GetCoins();
        coinsText.text = totalCoins.ToString();
        
        // Atualiza texto de cada item
        /*foreach (var text in itemTexts)
        {
            if (totalCoins >= itemCost)
                text.text = "EQUIPAR";
            else
                text.text = itemCost + " MOEDAS";
        }

        foreach (var coin in coinIcons)
        {
            coin.SetActive(false);
        }*/

        for (int i = 0; i < itemTexts.Length; i++)
        {
            bool isPurchased = PlayerPrefs.GetInt($"ItemPurchased_{i}", 0) == 1;
            bool isEquipped = PlayerPrefs.GetInt($"ItemEquipped_{i}", 0) == 1;

            if (isPurchased)
            {
                itemTexts[i].text = isEquipped ? "REMOVER" : "EQUIPAR";
                // coinIcons[i].SetActive(isEquipped);
                Destroy(coinIcons[i]);
            }
            else
            {
                itemTexts[i].text = itemCost.ToString();
                coinIcons[i].SetActive(!isEquipped);
            }
        }
    }

    private void OnItemClick(int index)
    {
        bool isPurchased = PlayerPrefs.GetInt($"ItemPurchased_{index}", 0) == 1;
        bool isEquipped = PlayerPrefs.GetInt($"ItemEquipped_{index}", 0) == 1;

        if (!isPurchased)
        {
            if (totalCoins >= itemCost)
            {
                sfxAcess.Audio(moneyClip);
                CoinController.Instance.SpendCoins(itemCost);
                PlayerPrefs.SetInt($"ItemPurchased_{index}", 1);
                PlayerPrefs.Save();
                UpdateUI();
            }
            else
            {
                sfxAcess.Audio(click);
            }
        }
        else
        {
            bool newEquipState = !isEquipped;
            PlayerPrefs.SetInt($"ItemEquipped_{index}", newEquipState ? 1 : 0);

            // Desequipa os outros
            if (newEquipState)
            {
                for (int i = 0; i < itemTexts.Length; i++)
                {
                    if (i != index)
                        PlayerPrefs.SetInt($"ItemEquipped_{i}", 0);
                }

                // ?? Salva qual checkpoint está equipado
                PlayerPrefs.SetInt("EquippedCheckpointIndex", index);
            }
            else
            {
                // ?? Remove checkpoint equipado se for dessequipado
                PlayerPrefs.DeleteKey("EquippedCheckpointIndex");
            }

            PlayerPrefs.Save();
            UpdateUI();
        }
    }


    // Caso queira atualizar ao abrir a loja (ex: quando a cena da loja é carregada)
    private void OnEnable()
    {
        if (CoinController.Instance != null)
            UpdateUI();
    }
}

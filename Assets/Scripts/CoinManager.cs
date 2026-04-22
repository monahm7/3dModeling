using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int coinCount = 0;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        Debug.Log("Coins now: " + coinCount);
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
        else
        {
            Debug.Log("coinText is NOT assigned in CoinManager");
        }
    }
}
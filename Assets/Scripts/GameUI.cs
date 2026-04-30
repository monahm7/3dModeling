using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    public TextMeshProUGUI coinText;
    int coins = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateUI();
            return true;
        }

        return false;
    }

    void UpdateUI()
    {
        coinText.text = "Coins: " + coins;
    }
}
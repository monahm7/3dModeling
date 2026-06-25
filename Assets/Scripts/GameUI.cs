using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    public TextMeshProUGUI coinText;
    int coins = 0;
    float birdViewReminderCooldown = 30f;
    float lastBirdViewReminderTime = -999f;

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

        if (coins >= 6 && Time.time - lastBirdViewReminderTime >= birdViewReminderCooldown)
        {
            lastBirdViewReminderTime = Time.time;
            FindFirstObjectByType<GameManager>().ShowBirdViewPopup();
        }
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

    public int GetCoins()
    {
    return coins;
    }

    void UpdateUI()
    {
        coinText.text = "Coins: " + coins;
    }
}
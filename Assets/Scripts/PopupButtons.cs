using UnityEngine;

public class PopupButtons : MonoBehaviour
{
    public GameObject weightPopup;
    public PlayerStats playerStats;

    public int coinCost = 8;
    public float weightLoss = 8f;

    public void ClosePopup()
    {
        weightPopup.SetActive(false);
    }

    public void BuyWeightLoss()
    {
        if (GameUI.instance.SpendCoins(coinCost))
        {
            playerStats.AddWeight(-weightLoss);
        }

        weightPopup.SetActive(false);
    }
}
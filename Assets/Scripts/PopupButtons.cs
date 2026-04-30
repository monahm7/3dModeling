using UnityEngine;

public class PopupButtons : MonoBehaviour
{
    public GameObject weightPopup;
    public PlayerStats playerStats;
    public PlayerMovement playerMovement;

    public int coinCost = 8;
    public float weightLoss = 8f;

    public void ClosePopup()
    {
        weightPopup.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;
    }

    public void BuyWeightLoss()
    {
        if (GameUI.instance.SpendCoins(coinCost))
        {
            playerStats.AddWeight(-weightLoss);
        }

        weightPopup.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;
    }

    void Update()
    {
        if (!weightPopup.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Y))
            BuyWeightLoss();

        if (Input.GetKeyDown(KeyCode.N))
            ClosePopup();
    }
}
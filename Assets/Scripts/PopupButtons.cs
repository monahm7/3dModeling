using UnityEngine;

public class PopupButtons : MonoBehaviour
{
    public GameObject weightPopup;
    public PlayerStats playerStats;

    public float weightLoss = 8f;

    public void ClosePopup()
    {
        weightPopup.SetActive(false);
    }

    public void BuyWeightLoss()
    {
        playerStats.AddWeight(-weightLoss);
        weightPopup.SetActive(false);
    }
}
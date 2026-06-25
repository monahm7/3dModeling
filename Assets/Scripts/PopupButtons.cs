using UnityEngine;
using UnityEngine.UI;

public class PopupButtons : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;

    private bool yesSelected = true;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;

    public Vector3 normalScale = Vector3.one;
    
    public Vector3 selectedScale = new Vector3(1.15f, 1.15f, 1f);
    
    public GameObject weightPopup;
    public PlayerStats playerStats;
    public PlayerMovement playerMovement;

    public int coinCost = 8;
    public float weightLoss = 8f;

      void Start()
    {
        SelectYes();
    }

    public void ShowPopup()
    {
        weightPopup.SetActive(true);

        if (playerMovement != null)
            playerMovement.enabled = false;

        SelectYes();
    }

    void SelectYes()
    {
        yesSelected = true;

        yesButton.transform.localScale = selectedScale;
        noButton.transform.localScale = normalScale;
    }

    void SelectNo()
    {
        yesSelected = false;

        yesButton.transform.localScale = normalScale;
        noButton.transform.localScale = selectedScale;
    }

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
            playerStats.ResetOverweightPopup();
        }

        weightPopup.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;
    }

    void Update()
    {
    if (!weightPopup.activeSelf) return;

    // Arrow key selection
    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
        SelectYes();
    }

    if (Input.GetKeyDown(KeyCode.RightArrow))
    {
        SelectNo();
    }

    // Enter confirms current selection
    if (Input.GetKeyDown(KeyCode.Return))
    {
        if (yesSelected)
        {
            BuyWeightLoss();
        }
        else
        {
            ClosePopup();
        }
    }
    }
}
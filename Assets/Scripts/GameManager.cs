using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject restartButton;
    public GameObject loseText;
    public GameObject continueButton;
    public PlayerMovement playerMovement;

    private int needCoinsCount = 0;

    void Start()
    {
        startMenu.SetActive(true);
        restartButton.SetActive(false);
        loseText.SetActive(false);
        continueButton.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = false;
    }

    public void StartGame()
    {
        startMenu.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;
    }

    public void ShowNeedCoins()
    {
        needCoinsCount++;

        loseText.SetActive(true);

        if (needCoinsCount < 3)
        {
            // First & second time
            loseText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oops!! Need More Coins!";
            continueButton.SetActive(true);
            restartButton.SetActive(false);
        }
        else
        {
            // Third time → real lose
            loseText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oops!! You Lost! Need More Coins!";
            continueButton.SetActive(false);
            restartButton.SetActive(true);
        }
    }

    public void ContinueGame()
    {
        loseText.SetActive(false);
        continueButton.SetActive(false);
        restartButton.SetActive(false);
    }

    public void ShowRestart()
    {
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
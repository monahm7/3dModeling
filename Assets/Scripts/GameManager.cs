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

    public void ShowRestart()
    {
        restartButton.SetActive(true);
    }

    public void ShowNeedCoins()
    {
        needCoinsCount++;

        loseText.SetActive(true);
        continueButton.SetActive(true);

        if (needCoinsCount >= 2)
            restartButton.SetActive(true);
    }

    public void ContinueGame()
    {
        loseText.SetActive(false);
        continueButton.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
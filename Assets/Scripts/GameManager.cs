using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject restartButton;
    public GameObject loseText;
    public GameObject continueButton;
    public TMPro.TextMeshProUGUI timerText;
    public PlayerMovement playerMovement;
    public float timerDuration = 45f;

    private int needCoinsCount = 0;

    private float currentTimer;
    private bool timerStarted = false;
    private bool gameFinished = false;

    void Start()
    {
        startMenu.SetActive(true);
        restartButton.SetActive(false);
        loseText.SetActive(false);
        continueButton.SetActive(false);
        currentTimer = timerDuration;
        timerText.text = "";

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

         if (!timerStarted)
        {
            timerStarted = true;
        }
        timerText.text = "Time: " + Mathf.Ceil(currentTimer).ToString();
    }

    public void ShowRestart()
    {
        restartButton.SetActive(true);
    }

    public void ShowWin()
    {
        gameFinished = true;

        loseText.SetActive(false);
        continueButton.SetActive(false);
        restartButton.SetActive(false);
    }
    void Update()
    {
        if (timerStarted && !gameFinished && !loseText.activeSelf)
        {
            currentTimer -= Time.deltaTime;

            timerText.text = "Time: " + Mathf.Ceil(currentTimer).ToString();

        if (currentTimer <= 0)
            {
                currentTimer = 0;
                timerText.text = "0";
                TimerFinished();
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void TimerFinished()
    {
        gameFinished = true;

        loseText.SetActive(true);
        loseText.GetComponent<TMPro.TextMeshProUGUI>().text = "Time's Up! You Lost!";

        continueButton.SetActive(false);
        restartButton.SetActive(true);

        if (playerMovement != null)
            playerMovement.enabled = false;
    }
}
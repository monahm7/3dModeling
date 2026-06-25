using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject restartButton;
    public GameObject loseText;
    public GameObject continueButton;
    public GameObject pauseButton;
    public GameObject pausePanel;
    public GameObject birdViewPopup;
    public TMPro.TextMeshProUGUI timerText;
    public PlayerMovement playerMovement;
    public GameObject winCelebration;
    public ParticleSystem winParticles;
    public GoalFlagVisibility goalFlagVisibility;
    public float timerDuration = 60f;

    private int needCoinsCount = 0;

    private float currentTimer;
    private bool timerStarted = false;
    private bool gameFinished = false;
    private bool canContinueAfterDelay = false;

    void Start()
    {
        startMenu.SetActive(true);
        restartButton.SetActive(false);
        loseText.SetActive(false);
        continueButton.SetActive(false);
        pauseButton.SetActive(false);
        birdViewPopup.SetActive(false);
        currentTimer = timerDuration;
        timerText.text = "";

        if (playerMovement != null)
            playerMovement.enabled = false;
    }

    public void StartGame()
   {
    startMenu.SetActive(false);
    
    pauseButton.SetActive(true);

    if (goalFlagVisibility != null)
        goalFlagVisibility.HideFlag();

    if (playerMovement != null)
        playerMovement.enabled = true;
    }

    public void ShowBirdViewPopup()
    {
        birdViewPopup.SetActive(true);
        StartCoroutine(HideBirdViewPopupAfterDelay());
    }
    System.Collections.IEnumerator HideBirdViewPopupAfterDelay()
    {
        yield return new WaitForSeconds(4f);

        birdViewPopup.SetActive(false);
    }

    public void ShowNeedCoins()
    {
         if (timerStarted && currentTimer <= 0)
        {
            TimerFinished();
            return;
        }
        
        needCoinsCount++;

        loseText.SetActive(true);

        canContinueAfterDelay = false;

        if (playerMovement != null)
            playerMovement.enabled = false;

        StartCoroutine(AllowContinueAfterDelay());

        loseText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oops!! Need More Coins!";
        continueButton.SetActive(true);
        restartButton.SetActive(false);
    }

    public void ContinueGame()
    {
        loseText.SetActive(false);
        continueButton.SetActive(false);
        restartButton.SetActive(false);

        if (playerMovement != null)
        playerMovement.enabled = true;

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
        Debug.Log("SHOW WIN CALLED");
        gameFinished = true;

        loseText.SetActive(false);
        continueButton.SetActive(false);
        restartButton.SetActive(true);
        pauseButton.SetActive(false);

        if (winCelebration != null)
            winCelebration.SetActive(true);

        if (winParticles != null)
        {
            winParticles.Clear();
            winParticles.Play();
        }

        if (playerMovement != null)
            playerMovement.enabled = false;
    }
    void Update()
   {
        if (startMenu.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }

        if (restartButton.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
          RestartGame();
        }

         if (continueButton.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
          ContinueGame();
        }

        if (canContinueAfterDelay && loseText.activeSelf)
        {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
             ContinueGame();
            }
        }

        if (timerStarted && !gameFinished && !loseText.activeSelf)
        {
            currentTimer -= Time.deltaTime;

            int timeInt = Mathf.CeilToInt(currentTimer);
            timerText.text = "Time: " + timeInt;

        if (timeInt <= 10)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = new Color(1f, 0.84f, 0.3f);
        }

        if (currentTimer <= 0)
        {
            currentTimer = 0;
            timerText.text = "Time: 0";
            TimerFinished();
        }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void TimerFinished()
    {
        gameFinished = true;
        canContinueAfterDelay = false;

        loseText.SetActive(true);
        loseText.GetComponent<TMPro.TextMeshProUGUI>().text = "Time's Up! You Lost!";
        
        continueButton.SetActive(false);
        restartButton.SetActive(true);
        pauseButton.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = false;
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }
    System.Collections.IEnumerator AllowContinueAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        canContinueAfterDelay = true;
    }
}
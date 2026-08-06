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
    public float timerIntroDuration = 2f;
    public float timerIntroFontSize = 110f;

    private RectTransform timerRect;
    private Vector2 timerOriginalAnchorMin;
    private Vector2 timerOriginalAnchorMax;
    private Vector2 timerOriginalPivot;
    private Vector2 timerOriginalPosition;
    private float timerOriginalFontSize;

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
        timerRect = timerText.rectTransform;

        timerOriginalAnchorMin = timerRect.anchorMin;
        timerOriginalAnchorMax = timerRect.anchorMax;
        timerOriginalPivot = timerRect.pivot;
        timerOriginalPosition = timerRect.anchoredPosition;
        timerOriginalFontSize = timerText.fontSize;

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
            StartCoroutine(ShowTimerIntro());
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
    System.Collections.IEnumerator ShowTimerIntro()
 {
    // Move timer to the center of the screen.
    timerRect.anchorMin = new Vector2(0.5f, 0.5f);
    timerRect.anchorMax = new Vector2(0.5f, 0.5f);
    timerRect.pivot = new Vector2(0.5f, 0.5f);
    timerRect.anchoredPosition = Vector2.zero;

    timerText.fontSize = timerIntroFontSize;
    timerText.alignment = TMPro.TextAlignmentOptions.Center;

    float elapsed = 0f;

    while (elapsed < timerIntroDuration)
    {
        elapsed += Time.deltaTime;

        // Creates a small bouncing/pulsing effect.
        float bounce = 1f + Mathf.Sin(elapsed * 12f) * 0.12f;
        timerRect.localScale = Vector3.one * bounce;

        yield return null;
    }

    // Return timer to its normal top-right position.
    timerRect.anchorMin = timerOriginalAnchorMin;
    timerRect.anchorMax = timerOriginalAnchorMax;
    timerRect.pivot = timerOriginalPivot;
    timerRect.anchoredPosition = timerOriginalPosition;

    timerText.fontSize = timerOriginalFontSize;
    timerText.alignment = TMPro.TextAlignmentOptions.Center;
    timerRect.localScale = Vector3.one;
    }
}
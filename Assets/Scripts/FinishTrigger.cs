using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public GameObject winText;
    public GameManager gameManager;

    public int requiredCoins = 18;   // 👈 you can tweak this

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentCoins = GameUI.instance.GetCoins();

            if (currentCoins >= requiredCoins)
            {
                // ✅ WIN
                winText.SetActive(true);

                if (gameManager != null)
                    gameManager.ShowWin();
            }
            else
            {
                // ❌ LOSE
                if (gameManager != null)
                    gameManager.ShowNeedCoins();
            }
        }
    }
}

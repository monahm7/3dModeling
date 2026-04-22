using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CoinManager.Instance != null)
            {
                CoinManager.Instance.AddCoin(value);
            }
            else
            {
                Debug.Log("CoinManager.Instance is NULL");
            }

            Destroy(gameObject);
        }
    }
}

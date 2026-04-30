using UnityEngine;

public class FatGateTrigger : MonoBehaviour
{
    public float requiredWeight = 12f;
    public GameObject weightPopup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null && stats.weight >= requiredWeight)
            {
                weightPopup.SetActive(true);

                PlayerMovement move = other.GetComponent<PlayerMovement>();
                if (move != null)
                {
                    move.enabled = false;
                }
            }
        }
    }
}
using UnityEngine;

public class FatGateTrigger : MonoBehaviour
{
    public float requiredWeight = 12f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null)
            {
                if (stats.weight >= requiredWeight)
                {
                    Debug.Log("Too heavy! Show popup here.");
                }
                else
                {
                    Debug.Log("Light enough. Pass.");
                }
            }
        }
    }
}
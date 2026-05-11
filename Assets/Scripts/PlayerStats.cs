using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float weight = 0f;

    Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    public void AddWeight(float amount)
    {
        weight += amount;
        Debug.Log("Player weight: " + weight);

        float grow = 1f + (weight * 0.015f);
        transform.localScale = new Vector3(
            startScale.x * grow,
            startScale.y,
            startScale.z * grow
        );
    }
}
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float weight = 0f;
    public float overweightLimit = 20f;
    private bool overweightPopupShown = false;

    Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    public void ResetOverweightPopup()
    {
        overweightPopupShown = false;
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

        if (weight >= overweightLimit && !overweightPopupShown)
        {
            overweightPopupShown = true;
            FindFirstObjectByType<PopupButtons>(FindObjectsInactive.Include).ShowPopup();
        }
    }
}
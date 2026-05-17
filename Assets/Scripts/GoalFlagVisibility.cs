using UnityEngine;

public class GoalFlagVisibility : MonoBehaviour
{
    public GameObject flagObject;

    void Start()
    {
        ShowFlag();
    }

    public void ShowFlag()
    {
        if (flagObject != null)
            flagObject.SetActive(true);
    }

    public void HideFlag()
    {
        if (flagObject != null)
            flagObject.SetActive(false);
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform flagTarget;
    public GameObject flagObject;
    public GoalFlagVisibility goalFlagVisibility;
    
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    public float smoothSpeed = 5f;
    public Vector3 birdViewOffset = new Vector3(0f, 15f, 0f);
    public float birdViewDuration = 4f;

    private bool birdViewActive = false;
    private float birdViewTimer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !birdViewActive)
        {
            if (GameUI.instance.SpendCoins(6))
        {
            birdViewActive = true;
            birdViewTimer = birdViewDuration;

            if (goalFlagVisibility != null)
            {
            goalFlagVisibility.ShowFlag();
            }
        }
        }

        if (birdViewActive)
        {
            birdViewTimer -= Time.deltaTime;

            if (birdViewTimer <= 0)
            {
               birdViewActive = false;

            if (goalFlagVisibility != null)
            {
                goalFlagVisibility.HideFlag();
            }
            }
        }
    }
    void LateUpdate()
   {
    if (target == null) return;

    Vector3 focusPoint = target.position;

    if (birdViewActive && flagTarget != null)
    {
        focusPoint = (target.position + flagTarget.position) / 2f;
    }

    Vector3 currentOffset = birdViewActive ? birdViewOffset : offset;
    Vector3 desiredPosition = focusPoint + currentOffset;

    transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

    if (birdViewActive && flagTarget != null)
    {
        transform.LookAt(focusPoint);
    }
    else
    {
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
    }
}

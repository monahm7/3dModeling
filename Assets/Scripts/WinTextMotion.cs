using UnityEngine;

public class WinTextMotion : MonoBehaviour
{
    public float targetY = 1.6f;
    public float riseSpeed = 0.4f;
    public float spinSpeed = 20f;
    public float floatAmount = 0.03f;
    public float floatSpeed = 1.5f;

    private Vector3 startPosition;
    private float baseY;
    private bool reachedTarget = false;

    void OnEnable()
    {
        startPosition = transform.position;
        baseY = startPosition.y;
        reachedTarget = false;
    }

    void Update()
    {
        if (!reachedTarget)
        {
            baseY = Mathf.MoveTowards(baseY, targetY, riseSpeed * Time.deltaTime);

            if (Mathf.Abs(baseY - targetY) < 0.01f)
            {
                baseY = targetY;
                reachedTarget = true;
            }
        }

        float floatY = reachedTarget
            ? Mathf.Sin(Time.time * floatSpeed) * floatAmount
            : 0f;

        transform.position = new Vector3(startPosition.x, baseY + floatY, startPosition.z);

        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + spinSpeed * Time.deltaTime,
            transform.rotation.eulerAngles.z
        );
    }
}
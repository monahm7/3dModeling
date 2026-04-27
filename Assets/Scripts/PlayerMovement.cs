using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Animator animator;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        float h = -Input.GetAxis("Horizontal");
        float v = -Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0f, v);

        if (move.magnitude > 0.1f)
        {
            controller.Move(move.normalized * moveSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;
    private Animator animator;
    private Rigidbody rb;

    [Header("Jump")]
    public float jumpForce = 7f;

    private bool isGrounded;

    void Start()
    {
        moveSpeed = DoofusDiaryLoader.Config.player_data.speed;

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(
            horizontal,
            0f,
            vertical
        ).normalized;

        // Move Doofus
        transform.Translate(
            movement * moveSpeed * Time.deltaTime,
            Space.World
        );

        // Rotate character toward movement direction
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }

        // Control walking animation
        animator.SetFloat("Speed", movement.magnitude);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;

            animator.SetTrigger("Jump");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pulpit"))
        {
            isGrounded = true;
        }
    }
}
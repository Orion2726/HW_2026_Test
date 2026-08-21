using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;
    private Animator animator;
    private Rigidbody rb;

    [Header("Jump")]
    public float jumpForce = 7f;

    private bool isGrounded;
    private int jumpCount = 0;

    private Transform cameraTransform;
    private ScoreManager scoreManager;

    void Start()
    {
        moveSpeed = DoofusDiaryLoader.Config.player_data.speed;

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        cameraTransform = Camera.main.transform;

        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Camera directions
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // Camera-relative movement
        Vector3 movement =
            (cameraForward * vertical) +
            (cameraRight * horizontal);

        movement = Vector3.ClampMagnitude(movement, 1f);

        // Move Doofus
        transform.Translate(
            movement * moveSpeed * Time.deltaTime,
            Space.World
        );

        // Rotate Doofus toward movement
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }
        // Push animation
        
        // Walking animation
        animator.SetFloat(
            "Speed",
            movement.magnitude
        );

        HandleJump();
    }

    void HandleJump()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        if (scoreManager == null)
            return;

        // No jump before 10 points
        if (!scoreManager.IsJumpUnlocked())
            return;

        // Single jump
        if (scoreManager.IsJumpUnlocked() &&
            !scoreManager.IsDoubleJumpUnlocked())
        {
            if (isGrounded)
            {
                PerformJump();
            }

            return;
        }

        // Double jump
        if (scoreManager.IsDoubleJumpUnlocked())
        {
            if (jumpCount < 2)
            {
                PerformJump();
            }
        }
    }

    void PerformJump()
    {
        rb.AddForce(
            Vector3.up * jumpForce,
            ForceMode.Impulse
        );

        jumpCount++;

        isGrounded = false;

        animator.SetTrigger("Jump");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pulpit"))
        {
            isGrounded = true;

            // Reset jumps when landing
            jumpCount = 0;
        }
    }
}
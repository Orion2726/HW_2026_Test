using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;
    private Animator animator;

    void Start()
    {
        moveSpeed = DoofusDiaryLoader.Config.player_data.speed;

        animator = GetComponentInChildren<Animator>();
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

        // Control animation
        animator.SetFloat("Speed", movement.magnitude);
    }
}
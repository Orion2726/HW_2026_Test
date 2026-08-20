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

        transform.Translate(
            movement * moveSpeed * Time.deltaTime,
            Space.World
        );

        float animationSpeed = movement.magnitude;

        animator.SetFloat("Speed", animationSpeed);
    }
}
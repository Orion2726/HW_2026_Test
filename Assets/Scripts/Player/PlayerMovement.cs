using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;

    void Start()
    {
        moveSpeed = DoofusDiaryLoader.Config.player_data.speed;
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
    }
}
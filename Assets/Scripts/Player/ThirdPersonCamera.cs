using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 6f, -8f);

    [Header("Spring Dynamics")]
    [Tooltip("How strongly the camera is pulled toward the target. Higher values make it snap faster.")]
    public float springStiffness = 20f;

    [Tooltip("How much the spring is slowed down. Lower values create more overshoot and bounciness.")]
    public float damping = 2f;

    // Tracks the current momentum of the camera
    private Vector3 currentVelocity;

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;

        // 1. Calculate the distance between current and desired positions
        Vector3 displacement = desiredPosition - transform.position;

        // 2. Apply Hooke's Law (Force = Stiffness * Displacement - Damping * Velocity)
        Vector3 springAcceleration = (displacement * springStiffness) - (currentVelocity * damping);

        // 3. Update velocity and apply it to the camera's position
        currentVelocity += springAcceleration * Time.deltaTime;
        transform.position += currentVelocity * Time.deltaTime;

        // Keep the camera focused on the player
        transform.LookAt(target);
    }
}
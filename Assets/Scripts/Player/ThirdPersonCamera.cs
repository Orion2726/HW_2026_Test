using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    [Header("Camera Distance")]
    public Vector3 normalOffset = new Vector3(0f, 6f, -8f);
    public Vector3 jumpOffset = new Vector3(0f, 7f, -10f);
    public Vector3 doubleJumpOffset = new Vector3(0f, 8f, -13f);

    [Header("Zoom Smoothing")]
    public float zoomSpeed = 4f;

    [Header("Rotation")]
    public float rotationSpeed = 5f;

    [Header("Spring Dynamics")]
    public float springStiffness = 20f;
    public float damping = 2f;

    private Vector3 currentVelocity;
    private Vector3 currentOffset;

    private ScoreManager scoreManager;

    private Vector3 desiredForward;

    void Start()
    {
        currentOffset = normalOffset;

        scoreManager = FindFirstObjectByType<ScoreManager>();

        // Start with camera's current forward direction
        desiredForward = target != null
            ? target.forward
            : Vector3.forward;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // Determine camera distance
        Vector3 targetOffset = GetTargetOffset();

        currentOffset = Vector3.Lerp(
            currentOffset,
            targetOffset,
            zoomSpeed * Time.deltaTime
        );

        // Rotate offset around player
        Quaternion targetRotation = Quaternion.LookRotation(
            desiredForward,
            Vector3.up
        );

        Vector3 rotatedOffset = targetRotation * currentOffset;

        Vector3 desiredPosition =
            target.position + rotatedOffset;

        // Spring movement
        Vector3 displacement =
            desiredPosition - transform.position;

        Vector3 springAcceleration =
            (displacement * springStiffness)
            - (currentVelocity * damping);

        currentVelocity +=
            springAcceleration * Time.deltaTime;

        transform.position +=
            currentVelocity * Time.deltaTime;

        // Look at player
        transform.LookAt(target);
    }

    Vector3 GetTargetOffset()
    {
        if (scoreManager == null)
            return normalOffset;

        if (scoreManager.Score >= 25)
            return doubleJumpOffset;

        if (scoreManager.Score >= 10)
            return jumpOffset;

        return normalOffset;
    }

    public void LookToward(Vector3 direction)
    {
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            return;

        desiredForward = direction.normalized;
    }
}
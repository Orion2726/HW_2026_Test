using UnityEngine;

public class PulpitSpawner : MonoBehaviour
{
    public GameObject pulpitPrefab;

    [Header("Normal Distance")]
    public float pulpitSize_1 = 6.7f;
    public float pulpitSize_2 = 6.51f;

    [Header("Jump Distance")]
    public float jumpPulpitSize_1 = 8.5f;
    public float jumpPulpitSize_2 = 8.3f;

    [Header("Double Jump Distance")]
    public float doubleJumpPulpitSize_1 = 10f;
    public float doubleJumpPulpitSize_2 = 9.8f;

    private Vector3 currentPulpitPosition;

    private Vector3 lastDirection = Vector3.zero;
    private ThirdPersonCamera cameraController;
    private ScoreManager scoreManager;

    void Start()
    {
        currentPulpitPosition = Vector3.zero;

        scoreManager = FindFirstObjectByType<ScoreManager>();
        cameraController = FindFirstObjectByType<ThirdPersonCamera>();
        SpawnPulpit(currentPulpitPosition);
    }

    public void SpawnNextPulpit()
    {
        Vector3 direction = GetRandomDirection();

        Vector3 previousPosition = currentPulpitPosition;

        Vector3 newPosition = currentPulpitPosition;

        float forwardDistance = GetForwardDistance();
        float sidewaysDistance = GetSidewaysDistance();

        if (direction == Vector3.forward)
        {
            newPosition += new Vector3(
                0f,
                0f,
                forwardDistance
            );
        }
        else if (direction == Vector3.back)
        {
            newPosition += new Vector3(
                0f,
                0f,
                -forwardDistance
            );
        }
        else if (direction == Vector3.right)
        {
            newPosition += new Vector3(
                sidewaysDistance,
                0f,
                0f
            );
        }
        else if (direction == Vector3.left)
        {
            newPosition += new Vector3(
                -sidewaysDistance,
                0f,
                0f
            );
        }

        SpawnPulpit(newPosition);

        // Tell camera where the new pulpit is
        if (cameraController != null)
        {
            Vector3 directionToNextPulpit =
                newPosition - previousPosition;

            cameraController.LookToward(
                directionToNextPulpit
            );
        }

        currentPulpitPosition = newPosition;

        lastDirection = direction;
    }

    Vector3 GetRandomDirection()
    {
        Vector3[] directions =
        {
            Vector3.forward,
            Vector3.back,
            Vector3.right,
            Vector3.left
        };

        Vector3 direction;

        do
        {
            direction = directions[Random.Range(0, directions.Length)];

        } while (direction == -lastDirection);

        return direction;
    }

    float GetForwardDistance()
    {
        if (scoreManager == null)
            return pulpitSize_1;

        if (scoreManager.Score >= 25)
            return doubleJumpPulpitSize_1;

        if (scoreManager.Score >= 10)
            return jumpPulpitSize_1;

        return pulpitSize_1;
    }

    float GetSidewaysDistance()
    {
        if (scoreManager == null)
            return pulpitSize_2;

        if (scoreManager.Score >= 25)
            return doubleJumpPulpitSize_2;

        if (scoreManager.Score >= 10)
            return jumpPulpitSize_2;

        return pulpitSize_2;
    }

    GameObject SpawnPulpit(Vector3 position)
    {
        return Instantiate(
            pulpitPrefab,
            position,
            Quaternion.identity
        );
    }
}
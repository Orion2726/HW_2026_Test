using UnityEngine;
public class BillboardUI : MonoBehaviour
{
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void LateUpdate()
    {
        if (mainCamera == null)
            return;
        Vector3 direction = transform.position - mainCamera.transform.position; // <-- flipped
        direction.y = 0f;
        if (direction.sqrMagnitude < 0.001f)
            return;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
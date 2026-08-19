using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0f, 6f, -8f);

    void Update()
    {
        if (target == null)
            return;

        transform.position = target.position + offset;

        transform.LookAt(target);
    }
}
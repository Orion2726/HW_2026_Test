using UnityEngine;

public class Pulpit : MonoBehaviour
{
    public float lifetime = 4f;

    private float timer;

    void Start()
    {
        timer = lifetime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class MenuDoofusAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = true;
    }
}
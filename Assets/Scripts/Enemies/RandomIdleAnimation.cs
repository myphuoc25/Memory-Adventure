using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    // Reference to the animator
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Randomize the idle animation
        if (!animator) return;

        // Get the current state
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Play the current state at a random time
        animator.Play(stateInfo.fullPathHash, -1, Random.Range(0f, 1f));

    }
}

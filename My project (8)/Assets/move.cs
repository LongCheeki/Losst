using System.Collections;
using UnityEngine;

public class KarlSequencePlayer : MonoBehaviour
{
    public Animator animator;

    [Header("Animation State Names")]
    public string idleState = "Idle";
    public string lookingState = "Looking";
    public string walkingState = "Walking";
    public string inspectState = "Inspect";

    [Header("Timing")]
    public float idleTime = 2f;
    public float firstLookingTime = 3f;
    public float firstWalkTime = 5f;
    public float inspectTime = 4f;
    public float secondWalkTime = 5f;

    [Header("Movement")]
    public float moveSpeed = 1.5f;

    private bool canMove = false;

    void Start()
    {
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        // Idle
        canMove = false;
        animator.Play(idleState);
        yield return new WaitForSeconds(idleTime);

        // Looking around
        animator.Play(lookingState);
        yield return new WaitForSeconds(firstLookingTime);

        // First walk toward temple
        animator.Play(walkingState);
        canMove = true;
        yield return new WaitForSeconds(firstWalkTime);

        // Stop and inspect
        canMove = false;
        animator.Play(inspectState);
        yield return new WaitForSeconds(inspectTime);

        // Continue walking
        animator.Play(walkingState);
        canMove = true;
        yield return new WaitForSeconds(secondWalkTime);

        // Final idle
        canMove = false;
        animator.Play(idleState);
    }

    void Update()
    {
        if (canMove)
        {
            Vector3 forward = transform.forward;
            forward.y = 0;
            forward.Normalize();

            transform.position += forward * moveSpeed * Time.deltaTime;
        }
    }
}
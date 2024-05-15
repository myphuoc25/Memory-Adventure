using Pathfinding;
using System.Collections;
using UnityEngine;

public class CompanisionAI : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 5f; // Determine the speed of the companion
    [SerializeField] private float nextWaypointDistance = 3f; // Determine the distance between the companion and the target

    private Transform target;
    private Seeker seeker;
    private Path path;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool reachedEndOfPath = false;
    private Coroutine moveCoroutine;

    private void Start()
    {
        target = FindAnyObjectByType<PlayerController>().transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        InvokeRepeating("CalculatePath", 0f, .5f);
    }

    /// <summary>
    /// Calculate the path to the target
    /// </summary>
    private void CalculatePath()
    {
        // If the seeker is done calculating the path
        if (seeker.IsDone())
        {
            // Start the path from the current position to the target position
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;

        while (currentWP < path.vectorPath.Count - 1)
        {

            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - rb.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWP]);
            if (distance < nextWaypointDistance)
                currentWP++;

            if (force.x != 0)
                if (force.x < 0)
                    spriteRenderer.transform.localScale = new Vector3(-1, 1, 0);
                else
                    spriteRenderer.transform.localScale = new Vector3(1, 1, 0);

            yield return null;
        }
    }

    private void FindEnemy()
    {

    }


}

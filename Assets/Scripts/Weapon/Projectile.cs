using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject hitVFXPrefab;
    [SerializeField] private bool isEnemyPojectle = false; // Determine if the projectile is from the enemy

    //private WeaponInfo weaponInfo;
    private Vector3 startPosition;
    private Vector3 moveDirection;
    private Rigidbody2D rb;
    private float projectileRange = 10f;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startPosition = transform.position;
        moveDirection = transform.right;
    }

    private void Update()
    {
        // Move the projectile
        //transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
        rb.velocity = moveSpeed * moveDirection;
        // Check if the projectile exceeds the weapon range
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if((playerHealth && isEnemyPojectle) && collision.CompareTag("Player"))
            {
                playerHealth?.TakeDamage(1, transform);
                var animationHit = Instantiate(hitVFXPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
                Destroy(animationHit, 2);
            } else if (!collision.isTrigger)
            {
                var animationHit = Instantiate(hitVFXPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
                Destroy(animationHit, 2);
            }
        
    }

    /// <summary>
    /// Destroy the projectile if it exceeds the weapon range
    /// </summary>
    private void DetectFireDistance()
    {
        if(Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}

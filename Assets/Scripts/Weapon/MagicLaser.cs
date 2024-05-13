using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 2f;
    [SerializeField] private GameObject hitVFXPrefab;

    private float laserRange;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        LaserFaceMouse();
    }

    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Debug.Log("Hit enemy");
        if (enemyHealth)
        {
            Debug.Log("Check health enemy");
            // Deal damage to the enemy
            //enemyHealth.TakeDamage(weaponInfo.weaponDamage);
            // Init animation for the enemy
            var animationHit = Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);
            // Destroy the projectile
            Destroy(gameObject);
            // Destroy the hit VFX after 2 seconds
            Destroy(animationHit, 2);
        }
    }

    private IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f;

        while (spriteRenderer.size.x < laserRange)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;

            // sprite 
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);

            // collider
            capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), capsuleCollider2D.size.y);
            capsuleCollider2D.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT)) / 2, capsuleCollider2D.offset.y);

            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void LaserFaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }

}

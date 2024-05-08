using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject hitVFXPrefab;

    private WeaponInfo weaponInfo;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Move the projectile
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
        // Check if the projectile exceeds the weapon range
        DetectFireDistance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth)
        {
            // Deal damage to the enemy
            enemyHealth.TakeDamage(weaponInfo.weaponDamage);
            // Init animation for the enemy
            Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);
            // Destroy the projectile
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Destroy the projectile if it exceeds the weapon range
    /// </summary>
    private void DetectFireDistance()
    {
        if(Vector3.Distance(startPosition, transform.position) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Update the weapon info of the projectile
    /// </summary>
    /// <param name="weaponInfo"></param>
    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }
}

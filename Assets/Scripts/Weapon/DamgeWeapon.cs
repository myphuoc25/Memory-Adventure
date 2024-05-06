using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeWeapon : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}

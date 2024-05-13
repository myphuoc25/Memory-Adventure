using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private float knockBackThrust = 15f; //xác định mức đẩy lùi khi kẻ địch bị tấn công. 
    [SerializeField] private float damgeRecoveryTime = 3f; //xác định thời gian hồi phục sau khi bị tấn công.

    private int currentHealth; // current health
    private KnockBack knockBack; // reference to KnockBack script
    private Flash flash; // reference to Flash script
    //private bool isInvulnerable = false; //determine if the player is invulnerable
    private bool canTakeDamage = true; //determine if the player can take damage

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the player is colliding with an enemy
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

        // If the player is colliding with an enemy and can take damage
        if (enemy && canTakeDamage)
        {
            // Take damage
            TakeDamage(1);
            // Knock back the player
            knockBack.GetKnockBack(collision.gameObject.transform, knockBackThrust);
            // Flash the player
            StartCoroutine(flash.FlashRoutine());
        }

    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            // Destroy the enemy current gameObject
            Destroy(gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        canTakeDamage = false;
        currentHealth -= damage;
        knockBack.GetKnockBack(transform, knockBackThrust);
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damgeRecoveryTime);
        canTakeDamage = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is colliding with an enemy
        if (other.gameObject.GetComponent<DamgeWeapon>() || other.gameObject.GetComponent<Projectile>()) //
        {
            GetComponent<PickUpSpawner>().DropItems();
            // Instantiate destroy VFX in the position of the destructible object when it dies
            var animationDeath = Instantiate(destroyVFX, transform.position, Quaternion.identity);
            // Destroy the destructible object
            Destroy(gameObject);
            // Destroy the destroy VFX after 2 seconds
            Destroy(animationDeath, 2);
        }
    }
}
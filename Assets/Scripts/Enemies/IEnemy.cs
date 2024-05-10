using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    int id { get; set; }
    int health { get; set; }
    void TakeDamage(int damage);
    void Die();
}

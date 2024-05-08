using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Staff Attack");
        StartCoroutine(AttackCDRoutine()); // start the attack cooldown routine
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }

    private void Awake()
    {
    }

    public void Start()
    {
    }

    private void Update()
    {
    }

}

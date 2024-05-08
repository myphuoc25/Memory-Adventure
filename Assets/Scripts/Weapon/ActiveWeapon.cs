using System.Collections;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    private bool attackButtonPressed, isAttacking = false;
    private PlayerControls playerControls;
    private float timeBetweenAttacks;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Action.Attack.started += ctx => attackButtonPressed = true;
        playerControls.Action.Attack.canceled += ctx => attackButtonPressed = false;

        AttackCooldown();
    }

    private void Update()
    {
        Attack();
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
    }

    private void Attack()
    {
        if(attackButtonPressed && !isAttacking)
        {
            isAttacking = true;
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }

    public void DestroyCurrentWeapon()
    {
        Debug.Log("Current weapon: " + CurrentActiveWeapon);
        if (CurrentActiveWeapon != null)
        {
            Destroy(CurrentActiveWeapon.gameObject);
            Debug.Log("Destroying current weapon");
        }
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        Debug.Log("New Weapon: " + newWeapon);
        CurrentActiveWeapon = newWeapon;

    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }
    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }


}

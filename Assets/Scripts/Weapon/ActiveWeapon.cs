using System.Collections;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    public bool attackButtonPressed, isAttacking = false;
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
        if(attackButtonPressed && !isAttacking && CurrentActiveWeapon)
        {
            //isAttacking = true;
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }

    public void DestroyCurrentWeapon()
    {
        if (CurrentActiveWeapon != null)
        {
            Destroy(CurrentActiveWeapon.gameObject);
        }
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;

        //Debug.Log(GameObject.FindGameObjectWithTag("Weapon"));
        //GameObject prefab = Resources.Load<GameObject>("Prefabs/Weapons/" + CurrentActiveWeapon.name);
        //GameObject[] prefabs = Resources.LoadAll<GameObject>("/Assets/Prefabs");
        //Debug.Log(prefabs.Length);
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

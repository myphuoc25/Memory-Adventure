using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeaponPlayer : Singleton<ManageWeaponPlayer>
{
    public WeaponManager weaponManager { get; private set; }
    public bool upgradeWeapon = false;
    

    protected override void Awake()
    {
        base.Awake();

        weaponManager = FindObjectOfType<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWeapon();
    }

    public void UpdateWeapon()
    {
        if(upgradeWeapon)
        {
            ActiveBag.Instance.UpdateInventorySlot();
            upgradeWeapon = false;
        }
    }

}

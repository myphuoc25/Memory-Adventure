using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;

    public WeaponInfo GetWeaponInfo() => weaponInfo;

    public void SetWeaponPrefab(GameObject weaponPrefab)
    {
        weaponInfo.weaponPrefab = weaponPrefab;
    }

}

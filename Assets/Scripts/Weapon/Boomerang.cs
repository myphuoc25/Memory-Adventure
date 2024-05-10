using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject boomerangPrefab;
    [SerializeField] private Transform throwSpawnPoint;

    public void Attack()
    {
        GameObject newBoomerang = Instantiate(boomerangPrefab, throwSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newBoomerang.GetComponent<ThrowWeapon>().UpdateWeaponInfo(weaponInfo);
        ActiveWeapon.Instance.gameObject.SetActive(false);
    }

    public WeaponInfo GetWeaponInfo() => weaponInfo;


}

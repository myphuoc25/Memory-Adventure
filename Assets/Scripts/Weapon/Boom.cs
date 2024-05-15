using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject boomPrefab;
    [SerializeField] private Transform boomSpawnPoint;

    public void Attack()
    {
        GameObject newBoomerang = Instantiate(boomPrefab, boomSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newBoomerang.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}

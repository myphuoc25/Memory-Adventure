using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;

    public WeaponInfo GetWeaponInfo() => weaponInfo;

    public void Attack()
    {
        Debug.Log("Staff Attack");
    }

    private void Update()
    {
        FlipItem();
    }

    public void FlipItem()
    {
        // Get the mouse position
        Vector3 mousePos = Input.mousePosition;

        // Convert the mouse position to world point and then to screen point. This helps us know the mouse position relative to the weapon's position.
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        // Calculate the angle between the weapon and the mouse position. This helps us rotate the weapon towards the mouse position.
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        // If the mouse position is on the left side of the weapon, flip the weapon to face the left side. Otherwise, flip the weapon to face the right side.
        if (mousePos.x < screenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        } else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}

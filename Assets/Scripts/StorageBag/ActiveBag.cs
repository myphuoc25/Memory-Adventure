using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBag : MonoBehaviour
{
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;

    private void Awake()
    {
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
        playerControls.Inventory.SwitchActiveBag.performed += ctx => ToggleActiveBag((int)ctx.ReadValue<float>());

        ToggleActiveBag(activeSlotIndexNum + 1);
    }

    private void ToggleActiveBag(int indexSlot)
    {
        // Turn off the current active bag
        transform.GetChild(activeSlotIndexNum).GetChild(0).gameObject.SetActive(false);

        // Set the new active bag
        activeSlotIndexNum = indexSlot - 1;

        transform.GetChild(activeSlotIndexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        // Destroy the current weapon
        ActiveWeapon.Instance.DestroyCurrentWeapon();

        // If the active slot is empty, return
        if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>())
        {
            Debug.Log("No weapon in this slot");
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponSlot = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().WeaponInfo.weaponPrefab;
        GameObject newWeapon = Instantiate(weaponSlot, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        newWeapon.transform.SetParent(ActiveWeapon.Instance.transform);
        
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
        
    }

}

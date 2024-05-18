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
        if(ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        // Destroy the current weapon
        //ActiveWeapon.Instance.DestroyCurrentWeapon();

        // If the active slot is empty, return
        //if (transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().WeaponInfo == null)
        //{
        //    Debug.Log("No weapon in this slot");
        //    ActiveWeapon.Instance.WeaponNull();
        //    return;
        //}

        //GameObject weaponSlot = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().WeaponInfo.weaponPrefab;

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.WeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if(weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        //GameObject newWeapon = Instantiate(weaponSlot, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.SetParent(ActiveWeapon.Instance.transform);
        
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
        
    }

}

using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaserPrefab;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator Animator;

    private readonly int AttackHash = Animator.StringToHash("attack");

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public WeaponInfo GetWeaponInfo() => weaponInfo;

    public void Attack()
    {
        Animator.SetTrigger(AttackHash);
        SpawnMagicLaser();
    }

    public void SpawnMagicLaser()
    {
        // Spawn the magic laser
        GameObject newLaser = Instantiate(magicLaserPrefab, magicLaserSpawnPoint.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
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

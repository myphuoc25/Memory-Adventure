using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimationPrefab; // This is the slash animation prefab
    [SerializeField] private Transform slashPoint; // This is the spawn point where the slash animation will be instantiated
    [SerializeField] private Transform weaponCollider; // This is the collider of the weapon

    private PlayerControls playerControls;
    private Animator animator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon; // This is the weapon that will be flipped

    private GameObject slashAnimation; // This is the instantiated slash animation

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();

        animator = GetComponent<Animator>();
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
        playerControls.Action.Attack.started += _ => Attack();
    }

    private void Update()
    {
        FlipSword();
    }

    private void Attack()
    {
        animator.SetTrigger("Attack"); // trigger the attack animation
        weaponCollider.gameObject.SetActive(true); // enable the weapon collider which allows weapons to collide with other objects in the game

        slashAnimation = Instantiate(slashAnimationPrefab, slashPoint.position, Quaternion.identity); // instantiate the slash animation at the slash point and set its rotation to the identity quaternion
        slashAnimation.transform.SetParent(slashPoint.parent); // set the slash animation's parent to the slash point's parent. This is done so that the slash animation will follow the player's movement
    }

    public void DoneAttackingAnimation()
    {
        weaponCollider.gameObject.SetActive(false); // disable the weapon collider
        //Destroy(slashAnimation);
    }

    public void SwingUpFlipAnimation()
    {
        slashAnimation.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if(playerController.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimation()
    {
        slashAnimation.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void FlipSword()
    {
        // Get the mouse position
        Vector3 mousePos = Input.mousePosition;

        // Convert the mouse position to world point and then to screen point. This helps us know the mouse position relative to the weapon's position.
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // Calculate the angle between the weapon and the mouse position. This helps us rotate the weapon towards the mouse position.
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        // If the mouse position is on the left side of the weapon, flip the weapon to face the left side. Otherwise, flip the weapon to face the right side.
        if (mousePos.x < screenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }


    }

}

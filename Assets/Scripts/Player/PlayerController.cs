using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float boostSpeed = 1.7f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer trailRenderer;

    private bool isBoosting = false;
    private bool isDashing = false;
    public bool FacingLeft { get; private set; } = false;
    public static PlayerController Instance;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator playerAnimator;
    private SpriteRenderer playerSprite;
    

    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponent <SpriteRenderer>();
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
        playerControls.Action.Dash.performed += _ => Dash();
    }

    private void Update()
    {
        StatePlayer();
        StateDirection();
    }

    private void FixedUpdate()
    {
        FlipSprite();
        Move();
    }

    // Method helper

    private void StatePlayer()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        if (playerControls.Movement.Behaviour.triggered)
        {
            isBoosting = true;
        }
        playerControls.Movement.Behaviour.canceled += _ => isBoosting = false;
    }

    private void StateDirection()
    {
        playerAnimator.SetFloat("moveX", movement.x);
        playerAnimator.SetFloat("moveY", movement.y);
    }
    
    private void FlipSprite()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x > playerScreenPoint.x)
        {
            playerSprite.flipX = false;
            FacingLeft = false;
        } else if (mousePos.x < playerScreenPoint.x)
        {
            playerSprite.flipX = true;
            FacingLeft = true;
        }
    }

    private void Move()
    {
        float currentMoveSpeed = moveSpeed;

        if (isBoosting)
        {
            currentMoveSpeed *= boostSpeed;
        }

        //rb.velocity = movement * moveSpeed;
        rb.MovePosition(rb.position + currentMoveSpeed * Time.fixedDeltaTime * movement);
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed /= dashSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }

}

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class playermovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float JumpStrength = 0.5f;
    public float playerSpeed = 0.5f;

    public Transform dashCD;
    public float coyote_range = 0.5f;
    private Animator _animator;
    private bool jumpRequested;
    public LayerMask ground;
    public TrailRenderer tr;
    public float dashTime = 0.2f;

    InputAction moveAction;
    InputAction jumpAction;

    private SpriteRenderer playerSprite;

    public float dash_cooldown = 4f;
    public float dash_str = 1f;
    float cooldown = 0f; // This is your timer
    private bool dashRequested; // Added this to pass the intent to FixedUpdate
    public Transform coyote_feet;
    void Start()
    {
        tr.emitting = false;
        ground = 1 << LayerMask.NameToLayer("Ground");
        playerSprite = gameObject.GetComponent<SpriteRenderer>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        _animator = GetComponent<Animator>();
        jumpRequested = false;
    }

    private void Update()
    {
        // 1. INPUT HANDLING
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        Collider2D groundCollider = Physics2D.OverlapCircle(
            coyote_feet.position,
            coyote_range,
            ground
        );

        if (jumpAction.WasPressedThisFrame() && groundCollider != null)
        {
            jumpRequested = true;

        }

        // FIXED: Checked against 'cooldown' instead of 'dash_cooldown'
        // Using GetKeyDown so it only fires once per press
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldown <= 0)
        {
            dashRequested = true;
            cooldown = dash_cooldown; // Reset timer
        }

        // Rotation logic
        if (moveValue.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (moveValue.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        setAnimation(moveValue.x);

        // Timer update
        if (cooldown > 0) cooldown -= Time.deltaTime;
        {

            dashCD.localScale = new Vector3(Mathf.Clamp(cooldown / dash_cooldown, 0, 1) * 0.25f, dashCD.localScale.y, dashCD.localScale.z);
        } 
    }
    

        void FixedUpdate()
        {
            // 2. PHYSICS HANDLING (Moved velocity and force here for consistency)
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            rb.linearVelocity = new Vector2(moveValue.x * playerSpeed, rb.linearVelocityY);

            if (jumpRequested)
            {
                StartCoroutine(jumpRoutine());
                rb.AddForce(Vector2.up * JumpStrength, ForceMode2D.Impulse);

                jumpRequested = false;
            }

            if (dashRequested)
            {

            

            //
            //rb.AddForce((new Vector2(dir * dash_str, rb.linearVelocityY)), ForceMode2D.Impulse);
            StartCoroutine(dashRoutine());
            dashRequested = false;
            }
        }

        void setAnimation(float moveX)
        {
            bool isMoving = !(moveX == 0);
            _animator.SetBool("isMoving", isMoving);
        }


        IEnumerator jumpRoutine()
        {
            //Do jump then wait for a bit before unsquashing
            Vector3 playerSize = transform.localScale;

            transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 0.90f, transform.localScale.z);

            yield return new WaitForSeconds(0.1f);

            transform.localScale = playerSize;
        }
        
        IEnumerator dashRoutine()
        {
        float dir = (transform.localRotation.eulerAngles.y >= 180) ? -1 : 1;
        float og_gravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(dir * dash_str, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = og_gravity;
        tr.emitting = false;

        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(coyote_feet.position, coyote_range);
        }
    }

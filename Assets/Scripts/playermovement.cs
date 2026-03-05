using UnityEngine;
using UnityEngine.InputSystem;

public class playermovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float JumpStrength = 0.5f;
    public float playerSpeed = 0.5f;


    private Animator _animator;
    private bool jumpRequested;

    InputAction moveAction;
    InputAction jumpAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        _animator = GetComponent<Animator>();
        jumpRequested = false;
    }

    private void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        rb.linearVelocity = new Vector2(moveValue.x * playerSpeed, rb.linearVelocityY);
        if (jumpAction.WasPressedThisFrame() && GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")))

        {

            // your jump code here
            jumpRequested = true;
            

        }
        if (rb.linearVelocityX < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (rb.linearVelocityX > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {

        }

        setAnimation();
    }
    // Update is called once per frame
    void FixedUpdate()
    {



        if (jumpRequested)
        {
            rb.AddForce(Vector2.up * JumpStrength, ForceMode2D.Impulse);
            jumpRequested = false;
        }



    }
     
    private void setAnimation()
    {
        bool isMoving =!( rb.linearVelocityX == 0);
        _animator.SetBool("isMoving", isMoving);
    }

}

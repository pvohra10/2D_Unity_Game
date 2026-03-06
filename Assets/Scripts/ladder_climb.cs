using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public float climbSpeed = 5f;
    private Rigidbody2D playerRb;
    private float originalGravity;

    // Detect when the player enters the ladder area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                originalGravity = playerRb.gravityScale;
                playerRb.gravityScale = 0; // Stop player from falling
            }
        }
    }

    // Handle the movement while inside the trigger
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerRb != null)
        {
            float verticalInput = Input.GetAxisRaw("Vertical"); // Works for W/S or Arrow Keys

            // Apply vertical velocity directly
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, verticalInput * climbSpeed);
        }
    }

    // Reset gravity when the player leaves the ladder
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerRb != null)
        {
            playerRb.gravityScale = originalGravity;
            playerRb = null;
        }
    }
}
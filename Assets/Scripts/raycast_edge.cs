using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 3f;
    public LayerMask groundLayer;
    public float edgeCheckDistance = 0.2f;

    [Header("References")]
    public Transform frontFoot;
    public Transform player;

    private Rigidbody2D rb;
    private Enemy enemyScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<Enemy>();

        // Find player automatically if not set
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        // Don't move if we are being knocked back or player is missing
        if (player == null || (enemyScript != null && enemyScript.isKnockedBack)) return;

        // 1. Calculate direction to player (X-axis only)
        float directionToPlayer = Mathf.Sign(player.position.x - transform.position.x);

        // 2. Check for ground at the "foot"
        bool isGroundAhead = Physics2D.OverlapCircle(frontFoot.position, edgeCheckDistance, groundLayer);

        // 3. Movement Logic
        if (isGroundAhead && (Mathf.Abs(player.position.x - transform.position.x) >= 0.5))
        {
            // Move toward player
            rb.linearVelocity = new Vector2(directionToPlayer * moveSpeed, rb.linearVelocity.y);
            Flip(directionToPlayer);
        }
        else
        {
            // STOP! We reached a ledge.
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void Flip(float dir)
    {
        // Adjust scale based on direction (assuming positive X is right)
        if (dir > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (dir < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void OnDrawGizmosSelected()
    {
        if (frontFoot != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(frontFoot.position, edgeCheckDistance);
        }
    }
}
using UnityEngine;
using System.Collections;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public Rigidbody2D rb;
    public GameObject bloodEffect;
    private Transform player;

    public SpriteRenderer sprite;
    public float knockBack_Str = 500f;
    public bool isKnockedBack;
    bool isDead;
    private GameObject impact;
    private AIPath ai;

    public LayerMask playerLayer;

    public float targettingRange = 50f;

    public AudioSource playerSpeaker;
    private AudioClip[] Aud;


    [Header("Death Settings")]
    [SerializeField] private EnemyDeathHandler deathHandler;

    void Start()
    {
        Aud = new AudioClip[1];
        Aud[0] = Resources.Load<AudioClip>("Music/final_slash");

        


        impact = Resources.Load<GameObject>("impact_anim_0");

        sprite = gameObject.GetComponent<SpriteRenderer>() ;
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Enemy: No object with the tag 'Player' found in the scene!");
        }

        playerSpeaker = playerObj.GetComponent<AudioSource>();

        // Try to get AIPath (enemy might not have it)
        //ploopert flimbus
        ai = GetComponent<AIPath>();

        isKnockedBack = false;
    }

    void Update()
    {
        Collider2D playerCheck = Physics2D.OverlapCircle(
            transform.position,
            targettingRange,
            playerLayer
        );

        if (playerCheck != null && !isKnockedBack)
        {
            if (ai != null) ai.canMove = true;
        }
        else
        {
            if (ai != null) ai.canMove = false;
        }





        if (health <= 0 && !isDead)
        {
            isDead = true;

            if (deathHandler != null)
            {
                deathHandler.ProcessDeath();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (!isKnockedBack && player != null)
        {
            HandleSpriteFlip();
        }
    }

    public void TakeDamage(float damage)
    {
        
        health -= damage;
        if (damage > 0)
        {
            playerSpeaker.PlayOneShot(Aud[0]);
            sprite.color = Color.red;
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }
        // Disable AI movement if it exists
        if (ai != null) ai.canMove = false;

        float pushDir = (transform.position.x < player.position.x) ? -1f : 1f;

        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = new Vector2(pushDir * knockBack_Str, 2f);

        Instantiate(impact, transform.position, Quaternion.identity);
        StartCoroutine(KnockbackDelay());
    }

    IEnumerator KnockbackDelay()
    {
        Vector3 scale = transform.localScale;
        transform.localScale =  new Vector3(transform.localScale.x * 0.75f, transform.localScale.y * 1.25f, transform.localScale.z);
        isKnockedBack = true;


        yield return new WaitForSeconds(0.25f);
        sprite.color = Color.white;
        transform.localScale = scale;
        yield return new WaitForSeconds(0.5f);

        isKnockedBack = false;

        // Re-enable AI if present
        if (ai != null) ai.canMove = true;

        
    }

    void HandleSpriteFlip()
    {
        //Get the current scale so we don't accidentally shrink or grow the enemy 
        Vector3 currentScale = transform.localScale;
        // Check where the player is 
        if (player.position.x > transform.position.x)
        {
            // Player is to the right: Force X scale to be NEGATIVE 
            // Mathf.Abs ensures we take the "size" and just add the minus sign 
            currentScale.x = -Mathf.Abs(currentScale.x);

        }
        else
        {
            // Player is to the left: Force X scale to be POSITIVE 
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        // Apply the scale back to the transform 
        transform.localScale = currentScale;

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, targettingRange);
    }
}
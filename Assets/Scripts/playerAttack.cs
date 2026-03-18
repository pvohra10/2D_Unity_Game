using UnityEngine;

using System.Collections;

using Utilities;

public class playerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public LayerMask enemyLayer;
    public Transform attackPos;


    public float attackRange;
    public float damage = 10;
    private Animator _animator;
    private bool atk;
    private float atkDelay = 0f;

    private SpriteRenderer sprite;
    public float playerHealth = 100f;

    bool blocking = false;

    public Rigidbody2D playerRb;

    public ScriptableObject death_handler;


    public SpriteRenderer sheild;


    public GameObject greenBar;

    public AudioSource playerSpeaker;
    private AudioClip[] Aud;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Aud = new AudioClip[3];
        Aud[0] = Resources.Load<AudioClip>("Music/slash_vintage");
        Aud[1] = Resources.Load<AudioClip>("Music/player_hurt");
        Aud[2] = Resources.Load<AudioClip>("Music/Sheild_Block");
        _animator = GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        atk = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Blocking Logic
        blocking = Input.GetKey(KeyCode.F) && !atk;

        // 2. Shield Visuals
        if (sheild != null)
        {
            sheild.enabled = blocking;
        }

        // 3. Attack Logic
        if (timeBtwAttack <= 0 && !blocking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                atk = true; // Set true to trigger animationd

                StartCoroutine(AttackDelay());
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                atk = false; // Ensure it's false if we aren't clicking
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
            atk = false; // Reset atk while waiting for cooldown
        }

        setAnimation(atk);
    }


    private void FixedUpdate()
    {


            sheild.enabled = blocking;
    }


    IEnumerator AttackDelay()
    {
        bool directionLeft = Utilities.tools.isFacingLeft(transform.localScale);

        if (directionLeft)
        {
            // Lunge Left
            playerRb.AddForce(new Vector2(-50, 0), ForceMode2D.Impulse);
        }
        else
        {
            // Lunge Right
            playerRb.AddForce(new Vector2(50, 0), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.42f); // delay to match animation



        playerSpeaker.PlayOneShot(Aud[0]);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(
            attackPos.position,
            attackRange,
            enemyLayer
        );

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position , attackRange);
    }

    void setAnimation(bool atk)
    {
        bool isAttacking = atk;
        _animator.SetBool("isAttacking", isAttacking);
    }

    
    public void TakeDamage(int damage)
    {

        if (!blocking)
        {
            playerSpeaker.PlayOneShot(Aud[1]);
            StartCoroutine(colorRoutine());
            playerHealth -= damage;
            if (playerHealth <= 0)
            {
                Destroy(gameObject);
            }

            float healthPercent = playerHealth / 100;
            greenBar.transform.localScale = new Vector3(healthPercent, 1, 1);
        }
        else
        {
            playerSpeaker.PlayOneShot(Aud[2]);
        }
    }

    IEnumerator colorRoutine()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }

    public bool returnblocking()
    {
        return blocking;
    }
}

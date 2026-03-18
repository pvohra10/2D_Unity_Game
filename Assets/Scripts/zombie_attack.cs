using UnityEngine;
using System.Collections;

public class zombie_attack : MonoBehaviour
{
    //Target Info
    public Transform player;
    public LayerMask playerLayer;

    //Attack Cooldown stuff
    public float cooldown = 2f;
    private float atk_cooldown = 0;
    private bool is_attacking = false;


    //Animation
    private Animator _animator;


    //Attack Hitbox stuff
    public Transform atk_hitbox;
    public float attackRange = 0.5f;
    public int attackDmg = 20;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(transform.position.x - player.position.x) <= 1.5f && atk_cooldown <= 0)
        {
            //Handle Attack
            is_attacking = true;
            StartCoroutine(AttackDelay());

            atk_cooldown = cooldown;
        }
        else
        {
            is_attacking = false;
        }

        atk_cooldown -= Time.deltaTime;


        setAnimation(is_attacking);
    }

    //Attacks
    IEnumerator AttackDelay()
    {
        bool blocked = false;
        yield return new WaitForSeconds(0.5f); // delay to match animation

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(
            atk_hitbox.position,
            attackRange,
            playerLayer
        );

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            blocked = enemiesToDamage[i].GetComponent<playerAttack>().returnblocking();
            enemiesToDamage[i].GetComponent<playerAttack>().TakeDamage(attackDmg);

        }


        if (blocked)
        {
            gameObject.GetComponent<Enemy>().TakeDamage(0);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atk_hitbox.position, attackRange);
    }

    void setAnimation(bool atk)
    {
        bool isAttacking = atk;
        _animator.SetBool("zombieIsAttacking", isAttacking);
    }

}



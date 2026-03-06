using UnityEngine;
using System.Collections;


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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        atk = false;
    }

    // Update is called once per frame
    void Update()
    {
        atk = false;

        Debug.Log("Update running");
        if (timeBtwAttack <= 0)
        {
            //Thenm you can attack
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Attack Started");

                atk = true;
                StartCoroutine(AttackDelay());

                //reset starting val for attacking
                timeBtwAttack = startTimeBtwAttack;
            }

        }
        else
        {
            //decrease cooldown
            timeBtwAttack -= Time.deltaTime;
        }
        setAnimation(atk);
    }


    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.42f); // delay to match animation

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
}

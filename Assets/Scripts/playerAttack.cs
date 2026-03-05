using UnityEngine;

public class playerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public LayerMask enemyLayer;
    public Transform attackPos;
    public float attackRange;
    public float damage = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            //Thenm you can attack
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer );
                for(int i =0; i<enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }



            //reset starting val for attacking
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            //decrease cooldown
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position , attackRange);
    }
}

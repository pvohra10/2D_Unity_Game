using UnityEngine;
using System.Collections;


public class Shoot_Fireball : MonoBehaviour
{
    public LayerMask playerLayer;
    private float timeBtwAttack;
    public float startTimeBtwAttack = 10f;

    public GameObject fireballPrefab; // Drag your prefab here in Inspector
    public Transform player;          // Drag your player object here
    public float fireForce = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            //Spawn Fireball and fire at player


                Shoot();
                //reset starting val for attacking
                timeBtwAttack = startTimeBtwAttack;

        }
        else
        {
            //decrease cooldown
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject ball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * fireForce;
    }

}

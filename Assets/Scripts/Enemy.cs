using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public Rigidbody2D rb;
    public GameObject bloodEffect;
    private Transform player;
    public float knockBack_Str = 0.5f;
    bool isKnockedBack;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("AIDestinationSetter: No object with the tag 'Player' found in the scene!");
        }
        isKnockedBack = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //float direction = Mathf.Sign(player.position.x - transform.position.x);

        //if ( Mathf.Abs(transform.position.x - player.position.x) > 2 && isKnockedBack == false) { 
        //    rb.linearVelocity = new Vector2(direction* speed, rb.linearVelocityY);
        //}
    }

    public void TakeDamage(float damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;

        // 1. Get a pure direction (-1 or 1) so distance doesn't matter
        float pushDir = (transform.position.x < player.position.x) ? -1f : 1f;

        // 2. Apply a consistent blast of velocity
        rb.linearVelocity = new Vector2(pushDir * knockBack_Str, rb.linearVelocity.y);

        StartCoroutine(KnockbackDelay());
    }

    IEnumerator KnockbackDelay()
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(1f); // delay to match animation
        isKnockedBack = false;
    }
}

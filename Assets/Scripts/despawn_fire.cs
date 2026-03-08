using UnityEngine;

public class despawn_fire : MonoBehaviour
{
    public BoxCollider2D collision_detect;
    public LayerMask playerLayer;
    public float fireBallRange = 0.5f;
    private Transform playerPos;
    public Rigidbody2D self;

    float suicide_timer = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        suicide_timer -= Time.deltaTime;
        if (suicide_timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger!");
            Collider2D[] playersToDamage = Physics2D.OverlapCircleAll(
                self.position,
                fireBallRange,
                playerLayer
            );

            for (int i = 0; i < playersToDamage.Length; i++)
            {
                playersToDamage[i].GetComponent<playerAttack>().TakeDamage(5);
                Destroy (gameObject);
                break;
            }

        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

    }

}

using UnityEngine;

public class DeathTouch : MonoBehaviour
{
    // Note: Parameter must be Collision2D, not GameObject
    void OnTriggerEnter2D(Collider2D collision)
    {
        // We access the .gameObject property of the collision data
        Destroy(collision.gameObject);
    }
}
using UnityEngine;

public class follow_player : MonoBehaviour
{

    public Transform player;
    public float yOffset = 1.75f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
    }
}

using UnityEngine;

public class checkPlayer : MonoBehaviour
{
    public float radius = 0.5f;
    public LayerMask player;
    public GameObject textBubble;
    public GameObject healthbargrn;
    public GameObject healthbarred;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Player Entered");
        if (other.CompareTag("Player"))
        {
            // Useful for things like poison damage or charging a zone
            textBubble.SetActive(true);
            healthbargrn.SetActive(false);
            healthbarred.SetActive(false);

        }




    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player Exit");
        if (other.CompareTag("Player"))
        {
            textBubble.SetActive(false);
            healthbargrn.SetActive(true);
            healthbarred.SetActive(true);
        }
    }
}

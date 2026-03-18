using UnityEngine;
using System.Collections;
public class destroyEffect : MonoBehaviour
{// Set this to the length of your animation (e.g., 0.2f or 0.5f)
    public float lifetime = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(delayAnim());
    }

    IEnumerator delayAnim()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
   
}

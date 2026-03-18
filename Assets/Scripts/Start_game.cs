using UnityEngine;

public class Start_game : MonoBehaviour
{
    public GameObject flambus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(flambus);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

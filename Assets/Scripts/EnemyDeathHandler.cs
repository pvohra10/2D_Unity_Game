using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Virtual means that child classes can override this
    public virtual void ProcessDeath()
    {
        //base one will do nothing

    }
}

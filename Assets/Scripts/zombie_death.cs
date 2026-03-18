using UnityEngine;
using System.Collections; // Added semicolon

public class zombie_death : EnemyDeathHandler
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>(); // Essential! Otherwise it crashes
    }

    public override void ProcessDeath()
    {
        Debug.Log("Zombie has died!");
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        _animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }
}
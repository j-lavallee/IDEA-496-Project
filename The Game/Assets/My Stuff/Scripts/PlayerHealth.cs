using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100;

    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("TakeDamage");

        if (health <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}

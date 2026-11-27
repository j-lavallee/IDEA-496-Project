using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    [SerializeField] float health = 100;

    [Header("Camera Shake Profiles")]
    [Tooltip("This profile is used for attacks")]
    [SerializeField] private CameraShakeProfile hitShake;
    [Tooltip("This profile is used on death")]
    [SerializeField] private CameraShakeProfile deathShake;

    Animator animator;
    public bool hasHyperArmor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (!hasHyperArmor)
        {
            animator.SetTrigger("TakeDamage");
            CameraShake.Instance.ShakeCamera(hitShake);
        }

        if (health <= 0 )
        {
            CameraShake.Instance.ShakeCamera(deathShake);
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    public void StartHyperArmor()
    {
        hasHyperArmor = true;
    }

    public void EndHyperArmor()
    {
        hasHyperArmor = false;
    }

}

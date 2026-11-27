using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] float health = 100;
    [SerializeField] float attackCooldown = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 100;

    [Header("Camera Shake Profiles")]
    [Tooltip("This profile is used for when the player is hit")]
    [SerializeField] private CameraShakeProfile hitShake;

    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCooldown = 0.5f;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        if (timePassed > attackCooldown)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
                timePassed = 0;
            }
        }

        timePassed += Time.deltaTime;

        if (newDestinationCooldown <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCooldown = 0.5f;
            agent.SetDestination(player.transform.position);
        }

        newDestinationCooldown -= Time.deltaTime;
        transform.LookAt(player.transform);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("TakeDamage");
        CameraShake.Instance.ShakeCamera(hitShake);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }

    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

}

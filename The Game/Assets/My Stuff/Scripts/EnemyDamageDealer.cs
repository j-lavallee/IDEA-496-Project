using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;

    [SerializeField] float weaponLength;
    [SerializeField] float weaponRadius;
    [SerializeField] float weaponDamage;
    [SerializeField] LayerMask playerLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDealDamage && !hasDealtDamage)
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, weaponRadius, transform.up, out hit, weaponLength, playerLayer))
            {
                if (hit.transform.TryGetComponent(out PlayerHealth playerHealth))
                {
                    playerHealth.TakeDamage(weaponDamage);
                    hasDealtDamage |= true;
                }
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage = false;
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 start = transform.position;
        Vector3 end = start + transform.up * weaponLength;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawWireSphere(start, weaponRadius);
        Gizmos.DrawWireSphere(end, weaponRadius);
    }

}

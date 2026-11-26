using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;

    [SerializeField] float weaponLength;
    [SerializeField] float weaponRadius;
    [SerializeField] float weaponDamage;
    [SerializeField] LayerMask targetLayers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDealDamage)
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, weaponRadius, transform.up, weaponLength, targetLayers);

            foreach (RaycastHit hit in hits) 
            {
                if (hit.transform.TryGetComponent(out EnemyController enemy) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    enemy.TakeDamage(weaponDamage);
                    hasDealtDamage.Add(hit.transform.gameObject);
                }
            }
            
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
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

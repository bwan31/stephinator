using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{    
    public Transform player;
    public NavMeshAgent agent;
    public LayerMask whatIsPlayer;

    public float health = 100;

    // attacking
    public float timeBetweenAttacks = 3;
    bool alreadyAttacked = false;
    public GameObject projectile;

    // states
    public float sightRange = 5, attackRange = 4;
    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void AttackPlayer() {
        agent.SetDestination(transform.position);

        // facing the target
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (!alreadyAttacked) {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 45f, ForceMode.Impulse);
            rb.AddForce(transform.up * 20f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack() {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy() {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        
        if (distance <= sightRange) {
            agent.SetDestination(player.position);
            if (distance <= attackRange) {
                AttackPlayer();
            }
        }
        // playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        // playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        // if (playerInSightRange && !playerInAttackRange) {
        //     if (distance <= sightRange) {
        //         agent.SetDestination(player.position);
        //     }
        // }
        // if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}

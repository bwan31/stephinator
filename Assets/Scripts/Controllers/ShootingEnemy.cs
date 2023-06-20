using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;

    [SerializeField] private float timer = 5;
    private float bulletTime;

    public GameObject enemyBullet;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position);
        ShootAtPlayer();
    }

    void ShootAtPlayer() {
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0) return;
        bulletTime = timer;
        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * 10000f);
        Destroy(bulletObj, 5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour, IDamageable
{
    public float health;
    public int attack;
    private float damageTimer = 0;
    public float damageDelay = .5f;
    public PlayerHealth player;

    private void Update()
    { 
        var atm = player.GetComponent<PlayerHealth>();

        // if(Input.GetKeyDown(KeyCode.B)) { // replace if condition with collision
        //    DealDamage(player.gameObject);
        // }
        damageTimer -= Time.deltaTime;
        if (health <= 0) {
            if(atm != null)
                atm.RestoreHealth(Regen());
            Destroy(gameObject);
        }
    }

    private float Regen() {
        return health/player.GetHealth;
    }

    public void TakeDamage(float amount) {
        health -= amount;
    }

    public void DealDamage (GameObject target) {
        var atm = target.GetComponent<PlayerHealth>();

        if(atm != null) {
            atm.TakeDamage(attack);
        }
    }

    void OnCollisionStay(Collision col) {
        if (col.collider.tag == "Player") 
        { 
            Debug.Log("hit detected"); 
            // Check if the player is currently able to take damage (i.e. the damage timer is less than or equal to zero) 
            if (damageTimer <= 0) 
            { 
                // Subtract the damage amount from the player's health 
                DealDamage(player.gameObject); 
 
                // Reset the damage timer 
                damageTimer = damageDelay;                 
            }
           
        }
    }
}
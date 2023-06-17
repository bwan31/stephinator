using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    public int health = 50;
    public int attack = 5;
    private float damageTimer = 0;
    public float damageDelay = .5f;
    public PlayerHealth player;


    private void Update()
    { 
        // if(Input.GetKeyDown(KeyCode.B)) { // replace if condition with collision
        //    DealDamage(player.gameObject);
        // }
        damageTimer -= Time.deltaTime;

    }

    public void TakeDamage(int amount) {
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
            Debug.Log("hit detecte"); 
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
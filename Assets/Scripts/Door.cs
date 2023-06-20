using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door: MonoBehaviour {
    public Animator animator;
    public Transform player;
    public Transform door;
    public Collider collider;

    void Start() {
        collider = GetComponent<Collider>();
    }


    void Update() {
        float distance = Vector3.Distance(player.position, door.position);

        animator.SetBool("Near", collider.OnTrigger);

        // idrk how this works


        // i need to know if the player crossed so that i can close the door but idk how
        

        if (distance<=25) {
            animator.SetBool("Near", true);
            player.HealDamage(100);
        } else {
            animator.SetBool("Near", false);
        }

    }
}
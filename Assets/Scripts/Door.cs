using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public Transform manager;

    private EnemyCount creeping;

    public Transform player;
    public Transform door;

    //public int num;
    //public Collider collider;

    void Start() {
        //collider = GetComponent<Collider>();
        creeping = manager.GetComponent<EnemyCount>();


    }


    void Update() {
        Collider col = gameObject.GetComponent<Collider>();

        Debug.Log(creeping);

        if(creeping.creep == 0) {
            Debug.Log("hello");
            col.isTrigger = true;
        }

        // //var atm = player.GetComponent<PlayerHealth>();

        // float distance = Vector3.Distance(player.position, door.position);

        // //animator.SetBool("Near", GetComponent<Collider>().OnTrigger);

        // // // idrk how this works


        // // // i need to know if the player crossed so that i can close the door but idk how
        

        // if (distance<=25) {
        //     animator.SetBool("Near", true);
        // } else {
        //     animator.SetBool("Near", false);
        // }

    }
    
    // void OnTriggerEnter(Collider other) {
    //     var atm = other.GetComponent<PlayerHealth>();
    //     Collider col = gameObject.GetComponent<Collider>();

    //     if(atm != null) {
    //         // code to close door
    //         atm.RestoreHealth(100);
    //         col.isTrigger = false;
    //         // StartCoroutine(ExecuteAfterTime(1));
    //     } 
    // }

    // IEnumerator ExecuteAfterTime(float time) {
    //     yield return new WaitForSeconds(time);
    //     Collider col = gameObject.GetComponent<Collider>();

    //     col.isTrigger = false;
    // }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherDoors : MonoBehaviour
{
    public Transform door;
    void OnTriggerEnter(Collider other) {
        //var atm = other.GetComponent<PlayerHealth>();
        // Collider col = gameObject.GetComponent<Collider>();

        //if(atm != null) {
            // code to close door
            //atm.RestoreHealth(50);
        if(other.gameObject.tag == "Player")
            StartCoroutine(ExecuteAfterTime(5));            // StartCoroutine(ExecuteAfterTime(1));
        //} 
    }

    IEnumerator ExecuteAfterTime(float time) {
        yield return new WaitForSeconds(time);
        Collider col = gameObject.GetComponent<Collider>();
        Debug.Log(time);
        col.isTrigger = false;
    }

}

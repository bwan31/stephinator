using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFloorMusic : MonoBehaviour
{
    public AudioSource audio;

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !audio.isPlaying) {
            Debug.Log("collider was hit");
            audio.Play();
        }
    }
}

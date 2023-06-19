using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float xRotation = 0f;
    public Camera cam;

    public float xSensitivity = .1f;
    public float ySensitivity = .1f;
    
    public void ProcessLook(Vector2 input) {
        float mouseX = input.x;
        float mouseY = input.y;
        // calculate camera rotatinon for looking up and down
        xRotation -= (mouseY) * ySensitivity; // * Time.deltaTime
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //apply this to our camera transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //rotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX) * xSensitivity); // * Time.deltaTime
    }
}

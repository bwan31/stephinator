using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float xRotation = 0f;
    public Camera cam;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    
    public void ProcessLook(Vector2 input) {
        float mouseX = input.x;
        float mouseY = input.y;
        // calculate camera rotatinon for looking up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //applythis to our camera transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //rotate player to lok left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;
    

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        Quaternion rotateX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotateY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion target = rotateX * rotateY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, smooth*Time.deltaTime);
    }
}

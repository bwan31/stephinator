using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked; // move to update when impmlementing pause menus or wtv
    }

    // Update is called once per frame
    void Update()
    {
        //if(!PauseMenu.isPaused) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; 
        // }
        // else {
        //     Cursor.visible = true;
        // }

    }
}

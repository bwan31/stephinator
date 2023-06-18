using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public static Action fireInput;

    private void Update() {
        if (Input.GetMouseButton(0)) {
            fireInput?.Invoke();
        }
    }
}

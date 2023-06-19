using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [Header("Refereces")]
    [SerializeField] private Transform[] weapons;

    [Header("Keys")]
    [SerializeField] private KeyCode[] keys;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private int selectedWeapon;
    private float lastSwitched;

    private void Start() {
        SetWeapon();
        Select(selectedWeapon);

        lastSwitched = 0f;
        
    }

    void Update() {
        int previousSelectedWeapon = selectedWeapon;

        for(int i = 0; i < keys.Length; i++) {
            if (Input.GetKeyDown(keys[i]) && lastSwitched >= switchTime) {
                selectedWeapon = i;
            }
        }

        if (previousSelectedWeapon != selectedWeapon) Select(selectedWeapon);

        lastSwitched += Time.deltaTime;
    }

    private void SetWeapon() {
        weapons = new Transform[transform.childCount];

        for (int i= 0; i < transform.childCount; i++) {
            weapons[i] = transform.GetChild(i);
        }

        if (keys == null) keys = new KeyCode[weapons.Length];

    }

    private void Select(int weaponIndex) {
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }

        lastSwitched = 0f;
        OnWeaponSelected();
    }

    private void OnWeaponSelected() {
        print("Selected new weapon");
    }
}

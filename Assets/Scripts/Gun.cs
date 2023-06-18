using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform guncam;
    float lastShot;

    private void Start() {
        PlayerFire.fireInput += Fire;
    }

    public void Fire() {
        RaycastHit hit;
        if (lastShot > 1f / (weaponData.fireRate / 60f)) {
            if (Physics.Raycast(guncam.position, guncam.forward, out hit)) {
                Debug.Log(hit.transform.name);
                if(hit.collider.tag == "enemy") {
                    print("enemy");
                }
                IDamageable damage = hit.transform.GetComponent<IDamageable>();
                damage?.TakeDamage(weaponData.damage);
            }

            lastShot = 0;
            OnGunFired();
        }
    }

    public void Update() {
        lastShot += Time.deltaTime;
        Debug.DrawRay(guncam.position, guncam.forward);
    }

    public void OnGunFired() {
    }
}

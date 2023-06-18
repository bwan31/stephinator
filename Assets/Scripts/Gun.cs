using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    bool isRecoil = false;
    public GameObject Revolve;
    private bool contact;

    [Header("References")]
    public AudioSource gunshot; // judgement destiny 2 
    public AudioSource gunshotHit; // sunset destiny 2 
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform guncam;
    float lastShot;

    private void Start() {
        PlayerFire.fireInput += Fire;
    }

    public void Fire() {
        if (!isRecoil) {
            StartCoroutine(StartRecoil());
        }
        RaycastHit hit;
        if (lastShot > 1f / (weaponData.fireRate / 60f)) {
            if (Physics.Raycast(guncam.position, guncam.forward, out hit)) {
                Debug.Log(hit.transform.name);
                if(hit.collider.tag == "enemy") {
                    gunshotHit.Play();
                    contact = true;
                }
                IDamageable damage = hit.transform.GetComponent<IDamageable>();
                damage?.TakeDamage(weaponData.damage);

            }
            if (contact != true)
                gunshot.Play();
            contact = false;

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

    IEnumerator StartRecoil()
    {
        Revolve.GetComponent<Animator>().Play("Recoil");
        isRecoil = true;
        yield return new WaitForSeconds(1f);
        Revolve.GetComponent<Animator>().Play("Rest");
        isRecoil = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public bool isRecoil = false;
    [SerializeField] private GameObject Revolve;
    private bool contact;

    [Header("References")]
    public AudioSource gunshot; // judgement destiny 2 
    public AudioSource gunshotHit; // sunset destiny 2 
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform guncam;
    [SerializeField] private Transform muzzle;
    [SerializeField] private TrailRenderer BulletTrail;
    [SerializeField] private float waitTime;

    float lastShot;

    private void Start() {
        PlayerFire.fireInput += Fire;
    }

    private void OnDisable() => isRecoil = false;

    public void Fire() {
        if (this.gameObject.activeSelf) {
            if (!isRecoil && this.gameObject.activeSelf) {
                if (weaponData.name == "Revolver") {
                    StartCoroutine(StartRecoil());
                }
                else if (weaponData.name == "Blade") {
                    StartCoroutine(StartRecoil2());
                }
            }
            RaycastHit hit;
            if (lastShot > 1f / (weaponData.fireRate / 60f)) {
                if (Physics.Raycast(guncam.position, guncam.forward, out hit, weaponData.maxDistance)) {
                    Debug.Log(hit.transform.name);
                    if(hit.collider.tag == "creep" || hit.collider.tag == "shoot" || hit.collider.tag == "snipe") {
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
                if (weaponData.name == "Revolver") {
                    TrailRenderer trail = Instantiate(BulletTrail, muzzle.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, hit));

                }
            }
        }
    }

    public void Update() {
        lastShot += Time.deltaTime;
        Debug.DrawRay(guncam.position, guncam.forward);
    }

    IEnumerator StartRecoil()
    {
        Revolve.GetComponent<Animator>().Play("Recoil");
        isRecoil = true;
        yield return new WaitForSeconds(waitTime);
        Revolve.GetComponent<Animator>().Play("Rest");
        isRecoil = false;
    }

    IEnumerator StartRecoil2()
    {
        Revolve.GetComponent<Animator>().Play("Swing");
        isRecoil = true;
        yield return new WaitForSeconds(waitTime);
        Revolve.GetComponent<Animator>().Play("Resting");
        isRecoil = false;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit) {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;
        while (time < 0.01) {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time+= Time.deltaTime;

            yield return null;
        }
        Trail.transform.position = Hit.point;
        Destroy(Trail.gameObject, Trail.time);
    }
}

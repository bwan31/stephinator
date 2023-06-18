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
    [SerializeField] private Transform muzzle;
    [SerializeField] private TrailRenderer BulletTrail;

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
            TrailRenderer trail = Instantiate(BulletTrail, muzzle.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
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
        yield return new WaitForSeconds(1f);
        Revolve.GetComponent<Animator>().Play("Rest");
        isRecoil = false;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit) {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;
        while (time < 0.1) {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time+= Time.deltaTime;

            yield return null;
        }
        Trail.transform.position = Hit.point;
        Destroy(Trail.gameObject, Trail.time);
    }
}

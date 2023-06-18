using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    
    [Header("Health Bar")]
    public float maxHealth = 100;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Damage Overlay")]
    public Image overlay; // our DamageOverlay GameObject
    public float duration; //how long the image fully stays opaque
    public float fadeSpeed; // how quickly the image will fade

    private float durationTimer; // timer to check against the duration

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);

    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if(overlay.color.a >  0) {            
            if(health < 30)
                return;
            
            durationTimer += Time.deltaTime;
            if(durationTimer > duration) {
                //fade image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }

        }
        // if(Input.GetKeyDown(KeyCode.A)) {  // change this stuff later to damage from enemies
        //     TakeDamage(Random.Range(5, 10));
        // }
        // if(Input.GetKeyDown(KeyCode.S)) {  // change this stuff later to damage from enemies
        //     RestoreHealth(100);
        // }

    }

    public void UpdateHealthUI() {
        //Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health/maxHealth;

        if(fillB >  hFraction) {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction) {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }


    }

    public void TakeDamage(float damage) {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);

    }

    public void RestoreHealth(float HealAmount) {
        health += HealAmount;
        lerpTimer = 0f;
    }


    // void onCollisionEnter(Collision col) {
    //     Debug.Log(col.gameObject);
    //     if(col.gameObject.name == "Enemy") {
    //         TakeDamage(5);
    //     }
    // }

    // void onTriggerEnter(Collider other) {
    //     Debug.Log("hit detected");
    //     if (other.gameObject.tag == "enemy") {
    //         TakeDamage(5);
    //     }
    // }
}

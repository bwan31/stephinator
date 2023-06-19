using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    CapsuleCollider collider;
    float originalHeight;
    public float reducedHeight;
    private bool sliding;
    Rigidbody rig;
    private float timer; 

    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 10f; // make sure this value is equal to line 62 
    public float acceleration = 4f;
    public float maxSpeed = 11;
    private bool isGrounded;
    public float gravity = -40f; //9.8
    public float jumpHeight = 1.5f;
    public bool lerpCrouch;
    public bool crouching;
    // public bool sprinting;
    public float crouchTimer;
    public float p;
    //public Vector3 moveDirection = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        collider = GetComponent<CapsuleCollider>();
        originalHeight = collider.height;
        rig = GetComponent<Rigidbody>();
    }
   
    // Update is called once per frame
    void Update()
    {   
        isGrounded = controller.isGrounded;
        if(lerpCrouch) { 
            crouchTimer += Time.deltaTime;
            p = crouchTimer / 1;
            p *= p;
            if(crouching || Input.GetKeyDown(KeyCode.LeftControl)) {
                // if (speed > 10) call slide
                if (speed>maxSpeed*.8) {
                    Sliding();
                }
                controller.height = Mathf.Lerp(controller.height, 1, p);
                // speed = 4f;
                Debug.Log(speed);
            } else {
                controller.height = Mathf.Lerp(controller.height, 2, p);     
                speed = 10;
            }
        }
        if (p > 1) {
            lerpCrouch = false;
            crouchTimer = 0f;
        }

    }

    public void Sliding() {
        sliding = true;
        collider.height = reducedHeight;
        rig.AddForce(transform.forward * speed * 5, ForceMode.VelocityChange);

        StartCoroutine(ExecuteAfterTime(2.5f));
    }

    IEnumerator ExecuteAfterTime(float time) {
        yield return new WaitForSeconds(time);
        
        GoUp();
    }
    
    IEnumerator ExecuteAfterTimeTwo(float time) {
        yield return new WaitForSeconds(time);
        
        speed = speed*3/4;
    }

    public void GoUp() {
        collider.height = originalHeight;

        speed = speed*3/4;

        StartCoroutine(ExecuteAfterTimeTwo(.5f));

        sliding = false;
    }

    //recieve the inputs for our InputManager.cs and apply them to our character controller.
    public void ProcessMove(Vector2 input) {
        if (true) {
            Vector3 moveDirection = Vector3.zero;
            if(speed < maxSpeed && crouching != true){
                speed += acceleration * Time.deltaTime;
            }

            //moveDirection.x = moveDirection.x + speed*Time.deltaTime;

            moveDirection.x = input.x;
            moveDirection.z = input.y;
            if ( transform.TransformDirection(moveDirection) * speed * Time.deltaTime == Vector3.zero) 
                speed = 10;
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);


            playerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && playerVelocity.y < 0)
                playerVelocity.y = -2f;
            controller.Move(playerVelocity * Time.deltaTime);
            //Debug.Log(playerVelocity.y);
        }
    }
    public void Jump() {
        if(isGrounded) {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch() {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    // public void Sprint() {
    //     // sprinting = !sprinting;
    //     // if(sprinting) {
    //     //     speed = 8;
    //     // } else {
    //     //     speed = 5;
    //     // }
    // }
}

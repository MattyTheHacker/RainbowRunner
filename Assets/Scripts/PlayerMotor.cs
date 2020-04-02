using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
    
    private CharacterController controller;
    private Vector3 moveVector;

    public float speed = 3.0f;

    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private float animationDuration = 3.0f;
    private float startTime;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start() {

        controller = GetComponent<CharacterController>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {

        if (isDead) {
            return;
        }

        if(Time.time - startTime < animationDuration) {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;
        if (controller.isGrounded) {
            verticalVelocity = -0.5f;
        } else {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        //x - left/right
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;

        //y - up and down
        moveVector.y = verticalVelocity;

        //z - forward and back
        moveVector.z = speed;
        controller.Move((moveVector * speed) * Time.deltaTime);
    }

    // change speed with given multiplier
    public void SetSpeed(float modifier) {
        speed = speed + (modifier/10);
        Debug.Log("Speed Increased! Now: " + speed);
    }

    // die on collision with enemy object
    // collect item on collision with collectable item
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Debug.Log("Player has collided with: " + hit.gameObject.tag);
        if (hit.point.z > transform.position.z && hit.gameObject.tag == "Enemy") {
            FindObjectOfType<AudioManager>().Stop("GameTheme");
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            Die();
        } else if (hit.point.z > transform.position.z && hit.gameObject.tag == "Collectable") {
            FindObjectOfType<AudioManager>().Play("CoinCollectEffect");
            GetComponent<Score>().OnCollect();
            Destroy(hit.gameObject);
        }
    }

    private void Die() {
        Debug.Log("Player has died.");
        isDead = true;
        GetComponent<Score>().OnDeath();
    }
}
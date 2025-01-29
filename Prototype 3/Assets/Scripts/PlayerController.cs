using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] public float speed;

    private Animator animator;

    private Rigidbody rb;
    private float gravity = 9.8f;

    private bool isGrounded = true;
    private bool gameIsActive = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravity;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && gameIsActive)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // jump sound
            // jump anim
            // stop dirt
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            // start dirt
            return;
        }

        if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameIsActive = false;
            Debug.Log("I just lost the game.");
            // death anim
            // explosion
        }
    }
}

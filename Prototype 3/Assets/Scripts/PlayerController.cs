using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [Header("Physics Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityModifier;
    [SerializeField] private float accelleration;

    private Rigidbody rb;
    private bool grounded = true;
    public float speed;
    private float speedInit;

    public bool gameIsActive = true;

    private Animator playerAnimator;

    [Header("Particles")]
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem dirt;

    [Header("Sounds")]
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip crash;

    private AudioSource playerAudio;

    void Start()
    {
        speedInit = speed;
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        if(!gameIsActive)
            return;

        float inputX = Input.GetAxis("Horizontal");
        speed = inputX * speedInit;

        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
            playerAnimator.SetBool("Jump_b", true);
            playerAnimator.SetTrigger("Jump_trig");
            dirt.Stop();
            playerAudio.PlayOneShot(jump, 1.0f);
        }

        speedInit *= 1 + (accelleration * Time.deltaTime);

        playerAnimator.SetFloat("Speed_f", Mathf.Abs(inputX));

        if(inputX == 0)
            dirt.Stop();
        else if(inputX != 0 && grounded)
            dirt.Play();

        if(Input.GetKey(KeyCode.LeftControl))
            playerAnimator.SetBool("Crouch_b", true);
        else
            playerAnimator.SetBool("Crouch_b", false);

        if(inputX == 0)
            StartCoroutine(nameof(Idling));
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameIsActive = false;
            Debug.Log("I just lost the game.");

            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);

            explosion.Play();
            dirt.Stop();
            playerAudio.PlayOneShot(crash, 1.0f);
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            playerAnimator.SetBool("Jump_b", false);

            if(gameIsActive)
                dirt.Play();
        }
    }

    IEnumerator Idling()
    {
        yield return new WaitForSeconds(2);

        if(Input.GetAxis("Horizontal") != 0)
            yield return null;

        playerAnimator.SetBool("Idle_b", true);
        int random = Random.Range(0, 3);
        playerAnimator.SetInteger("Idle_int", random);

        yield return new WaitForSeconds(2);
        playerAnimator.SetBool("Idle_b", false);
    }
}

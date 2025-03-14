using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] private float leftBound = -15;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if(!playerController.gameIsActive)
            return;

        transform.Translate(playerController.speed * Time.deltaTime * Vector3.left);

        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }
}

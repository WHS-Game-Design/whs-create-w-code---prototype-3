using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        transform.Translate(Vector3.left * playerController.speed * Time.deltaTime);
    }
}

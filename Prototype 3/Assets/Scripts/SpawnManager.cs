using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private float spawnRate;

    private PlayerController playerController;

    private readonly Vector3 spawnPosition = new(30, 0, 0);

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(nameof(SpawnObstacles));
    }

    void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
    }

    IEnumerator SpawnObstacles()
    {
        while(playerController.gameIsActive)
        {
            float wait = Random.Range(spawnRate / 2, spawnRate * 2);
            yield return new WaitForSeconds(wait);
            SpawnObstacle();
        }
    }
}

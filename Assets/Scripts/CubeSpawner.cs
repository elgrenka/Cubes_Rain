using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const float SpawnInterval = 0.2f;
    private const float SpawnAreaWidth = 20f;
    private const float SpawnAreaLength = 20f;
    private const float SpawnHeight = 15f;
    private const float Half = 0.5f;
    private const float TimeZero = 0f;

    [SerializeField] private CubePool cubePool;
    [SerializeField] private float spawnInterval = SpawnInterval;
    [SerializeField] private float spawnAreaWidth = SpawnAreaWidth;
    [SerializeField] private float spawnAreaLength = SpawnAreaLength;
    [SerializeField] private float spawnHeight = SpawnHeight;
    [SerializeField] private Color initialCubeColor = Color.crimson;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnCube();
            spawnTimer = TimeZero;
        }
    }

    private void SpawnCube()
    {
        float randomX = Random.Range(-spawnAreaWidth * Half, spawnAreaWidth * Half + 1);
        float randomZ = Random.Range(-spawnAreaLength * Half, spawnAreaLength * Half + 1);

        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, randomZ);

        FallingCube cube = cubePool.GetCube();
        cube.Activate(spawnPosition, initialCubeColor);
    }
}
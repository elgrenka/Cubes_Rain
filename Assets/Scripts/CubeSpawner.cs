using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const float SpawnInterval = 0.2f;
    private const float SpawnAreaWidth = 20f;
    private const float SpawnAreaLength = 20f;
    private const float SpawnHeight = 15f;
    private const float Half = 0.5f;

    [SerializeField] private CubePool _cubePool;
    [SerializeField] private float _spawnInterval = SpawnInterval;
    [SerializeField] private float _spawnAreaWidth = SpawnAreaWidth;
    [SerializeField] private float _spawnAreaLength = SpawnAreaLength;
    [SerializeField] private float _spawnHeight = SpawnHeight;
    [SerializeField] private Color _initialCubeColor = Color.crimson;

    private Coroutine _spawnRoutine;

    private void OnEnable()
    {
        _spawnRoutine = StartCoroutine(SpawnCubesRoutine());
    }

    private void OnDisable()
    {
        if (_spawnRoutine is not null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    private IEnumerator SpawnCubesRoutine()
    {
        yield return new WaitForSeconds(_spawnInterval);

        SpawnCube();

        if (isActiveAndEnabled)
            _spawnRoutine = StartCoroutine(SpawnCubesRoutine());
    }

    private void SpawnCube()
    {
        float randomX = Random.Range(-_spawnAreaWidth * Half, _spawnAreaWidth * Half + 1);
        float randomZ = Random.Range(-_spawnAreaLength * Half, _spawnAreaLength * Half + 1);

        Vector3 spawnPosition = new Vector3(randomX, _spawnHeight, randomZ);

        FallingCube cube = _cubePool.GetCube();
        cube.Activate(spawnPosition, _initialCubeColor);
    }
}
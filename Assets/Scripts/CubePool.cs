using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    private const int InitialPoolSize = 50;

    [SerializeField] private FallingCube cubePrefab;
    [SerializeField] private int initialPoolSize = InitialPoolSize;

    private readonly Queue<FallingCube> availableCubes = new();

    public FallingCube GetCube()
    {
        if (availableCubes.Count == 0)
            availableCubes.Enqueue(CreateCube());

        return availableCubes.Dequeue();
    }

    public void ReturnCube(FallingCube cube)
    {
        cube.gameObject.SetActive(false);
        availableCubes.Enqueue(cube);
    }

    private void Awake()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            FallingCube cube = CreateCube();
            cube.gameObject.SetActive(false);
            availableCubes.Enqueue(cube);
        }
    }

    private FallingCube CreateCube()
    {
        FallingCube cube = Instantiate(cubePrefab, transform);
        cube.Initialize(this);
        return cube;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    private const int InitialPoolSize = 50;

    [SerializeField] private FallingCube cubePrefab;
    [SerializeField] private int initialPoolSize = InitialPoolSize;

    private readonly Queue<FallingCube> availableCubes = new();
    private readonly List<FallingCube> createdCubes = new();

    public FallingCube GetCube()
    {
        if (availableCubes.Count == 0)
            availableCubes.Enqueue(CreateCube());

        return availableCubes.Dequeue();
    }

    private void ReturnCube(FallingCube cube)
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
        cube.LifeTimeExpired += OnCubeLifeTimeExpired;

        createdCubes.Add(cube);

        return cube;
    }

    private void OnCubeLifeTimeExpired(FallingCube cube)
    {
        ReturnCube(cube);
    }

    private void OnDestroy()
    {
        foreach (FallingCube cube in createdCubes)
        {
            if (cube is not null)
            {
                cube.LifeTimeExpired -= OnCubeLifeTimeExpired;
            }
        }
    }
}
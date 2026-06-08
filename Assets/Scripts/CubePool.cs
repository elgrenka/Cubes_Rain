using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    private const int InitialPoolSize = 50;

    [SerializeField] private FallingCube _cubePrefab;
    [SerializeField] private int _initialPoolSize = InitialPoolSize;

    private readonly Queue<FallingCube> _availableCubes = new();
    private readonly List<FallingCube> _createdCubes = new();

    private void Awake()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            FallingCube cube = CreateCube();
            cube.gameObject.SetActive(false);
            _availableCubes.Enqueue(cube);
        }
    }

    private void OnDestroy()
    {
        foreach (FallingCube cube in _createdCubes)
        {
            if (cube is not null)
            {
                cube.LifeTimeExpired -= OnCubeLifeTimeExpired;
            }
        }
    }

    public FallingCube GetCube()
    {
        if (_availableCubes.Count == 0)
            _availableCubes.Enqueue(CreateCube());

        return _availableCubes.Dequeue();
    }

    private void ReturnCube(FallingCube cube)
    {
        cube.gameObject.SetActive(false);
        _availableCubes.Enqueue(cube);
    }

    private FallingCube CreateCube()
    {
        FallingCube cube = Instantiate(_cubePrefab, transform);
        cube.LifeTimeExpired += OnCubeLifeTimeExpired;

        _createdCubes.Add(cube);

        return cube;
    }

    private void OnCubeLifeTimeExpired(FallingCube cube)
    {
        ReturnCube(cube);
    }
}
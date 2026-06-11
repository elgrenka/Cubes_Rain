using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    private const int InitialPoolSize = 50;

    [SerializeField] private FallingCube _cubePrefab;
    [SerializeField] private int _initialPoolSize = InitialPoolSize;

    private readonly Queue<FallingCube> _availableCubes = new();

    private void Awake()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            FallingCube cube = CreateCube();
            cube.Deactivate();
            _availableCubes.Enqueue(cube);
        }
    }

    public FallingCube GetCube()
    {
        if (_availableCubes.Count == 0)
            _availableCubes.Enqueue(CreateCube());

        return _availableCubes.Dequeue();
    }

    public void ReturnCube(FallingCube cube)
    {
        cube.Deactivate();
        _availableCubes.Enqueue(cube);
    }

    private FallingCube CreateCube()
    {
        return Instantiate(_cubePrefab, transform);
    }
}
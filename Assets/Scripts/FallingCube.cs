using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallingCube : MonoBehaviour
{
    private const float TimeZero = 0f;
    private const float MinLifeTime = 2f;
    private const float MaxLifeTime = 5f;

    [SerializeField] private Renderer _cubeRenderer;

    private bool _hasColorChanged;
    private float _lifeTime;
    private Coroutine _lifeTimeRoutine;

    public event Action<FallingCube> LifeTimeExpired;

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasColorChanged)
            return;

        if (collision.gameObject.TryGetComponent<Platform>(out _) == false)
            return;

        _hasColorChanged = true;
        _cubeRenderer.material.color = Random.ColorHSV();
        _lifeTime = Random.Range(MinLifeTime, MaxLifeTime + 1);
        _lifeTimeRoutine = StartCoroutine(LifeTimeRoutine());
    }

    private void OnDisable()
    {
        StopLifeTimeRoutine();
    }

    public void Activate(Vector3 spawnPosition, Color startColor)
    {
        StopLifeTimeRoutine();

        transform.position = spawnPosition;
        _cubeRenderer.material.color = startColor;
        _hasColorChanged = false;
        _lifeTime = TimeZero;

        gameObject.SetActive(true);
    }

    private IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(_lifeTime);

        LifeTimeExpired?.Invoke(this);
    }

    private void StopLifeTimeRoutine()
    {
        if (_lifeTimeRoutine is not null)
        {
            StopCoroutine(_lifeTimeRoutine);
            _lifeTimeRoutine = null;
        }
    }
}
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallingCube : MonoBehaviour
{
    private const float MinLifeTime = 2f;
    private const float MaxLifeTime = 5f;

    [SerializeField] private Renderer _cubeRenderer;

    private bool _hasColorChanged;
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

        float lifeTime = Random.Range(MinLifeTime, MaxLifeTime);
        _lifeTimeRoutine = StartCoroutine(LifeTimeRoutine(lifeTime));
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

        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator LifeTimeRoutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

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
using UnityEngine;

public class FallingCube : MonoBehaviour
{
    private const float TimeZero = 0f;
    private const float MinLifeTime = 2f;
    private const float MaxLifeTime = 5f;
    private const string CubePlatformTag = "Platform";
    
    [SerializeField] private Renderer cubeRenderer;

    private CubePool cubePool;

    private bool hasColorChanged;
    private float lifeTime;
    private float lifeTimeTimer;

    public void Initialize(CubePool pool)
    {
        cubePool = pool;
    }

    public void Activate(Vector3 spawnPosition, Color startColor)
    {
        transform.position = spawnPosition;

        cubeRenderer.material.color = startColor;

        hasColorChanged = false;
        lifeTimeTimer = TimeZero;
        lifeTime = TimeZero;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (hasColorChanged == false)
            return;

        lifeTimeTimer += Time.deltaTime;

        if (lifeTimeTimer >= lifeTime)
            cubePool.ReturnCube(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasColorChanged)
            return;

        if (collision.gameObject.CompareTag(CubePlatformTag) == false)
            return;

        hasColorChanged = true;
        cubeRenderer.material.color = Random.ColorHSV();
        lifeTime = Random.Range(MinLifeTime, MaxLifeTime + 1);
    }
}
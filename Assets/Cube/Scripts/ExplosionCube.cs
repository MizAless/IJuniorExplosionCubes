using UnityEngine;

[RequireComponent(typeof(Rigidbody))]  
public class ExplosionCube : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _scaleRedutionCoefficient;
    [SerializeField] private float _splitChanse = 1f;
    [SerializeField] private float _splitChanseRedutionCoefficient = 2f;
    [SerializeField] private int _minSpawnedCubesCount;
    [SerializeField] private int _maxSpawnedCubesCount;
    [SerializeField] private ExplosionCube _explosionCubePrefab;

    private void Start()
    {
        SetRandomColor();
    }

    private void OnMouseDown()
    {
        float splitRoll = Random.value;

        if (splitRoll <= _splitChanse)
            Explode();

        Destroy(gameObject);
    }

    private void OnValidate()
    {
        if (_minSpawnedCubesCount < 0)
            _minSpawnedCubesCount = 0;

        if (_maxSpawnedCubesCount < _minSpawnedCubesCount)
            _maxSpawnedCubesCount = _minSpawnedCubesCount + 1;
    }

    private void Explode()
    {
        int spawnedCubesCount = Random.Range(_minSpawnedCubesCount, _maxSpawnedCubesCount + 1);

        for (int i = 0; i < spawnedCubesCount; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Vector3 newExplosionCubeLocalScale = transform.localScale / _scaleRedutionCoefficient;

        ExplosionCube newExplosionCube = Instantiate(_explosionCubePrefab);

        newExplosionCube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        newExplosionCube.transform.localScale = newExplosionCubeLocalScale;
        newExplosionCube._splitChanse /= _splitChanseRedutionCoefficient;
        newExplosionCube.SetRandomColor();
    }

    private void SetRandomColor()
    {
        float randomRedValue = Random.value;
        float randomGreenValue = Random.value;
        float randomBlueValue = Random.value;
        float alphaValue = 1f;

        Color newColor = new Color(randomRedValue, randomGreenValue, randomBlueValue, alphaValue);

        GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }
}

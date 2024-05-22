using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _scaleRedutionCoefficient;
    [SerializeField] private float _splitChanceRedutionCoefficient = 2f;
    [SerializeField] private int _minSpawnedCubesCount;
    [SerializeField] private int _maxSpawnedCubesCount;
    [SerializeField] private ExplosionCube _explosionCubePrefab;
    [SerializeField] private List<ExplosionCube> _explosionCubesList;
    [SerializeField] private Exploder _exploder;

    private void Awake()
    {
        foreach (ExplosionCube explosionCube in _explosionCubesList)
        {
            explosionCube.Destroyed += Spawn;
        }
    }

    private void OnValidate()
    {
        if (_minSpawnedCubesCount < 0)
            _minSpawnedCubesCount = 0;

        if (_maxSpawnedCubesCount < _minSpawnedCubesCount)
            _maxSpawnedCubesCount = _minSpawnedCubesCount + 1;
    }

    public void Spawn(ExplosionCube explosionCube)
    {
        int spawnedCubesCount = UnityEngine.Random.Range(_minSpawnedCubesCount, _maxSpawnedCubesCount + 1);

        List<ExplosionCube> explosionCubes = new List<ExplosionCube>();

        for (int i = 0; i < spawnedCubesCount; i++)
        {
            Vector3 newExplosionCubeLocalScale = explosionCube.transform.localScale / _scaleRedutionCoefficient;
            float newSplitChance = explosionCube.SplitChanse / _splitChanceRedutionCoefficient;

            ExplosionCube newExplosionCube = Instantiate(_explosionCubePrefab, explosionCube.transform.position, Quaternion.identity);
            newExplosionCube.Init(newExplosionCubeLocalScale, newSplitChance);
            newExplosionCube.Destroyed += Spawn;

            explosionCubes.Add(newExplosionCube);
        }

        _exploder.Explode(explosionCube.transform.position, explosionCubes);
    }
}

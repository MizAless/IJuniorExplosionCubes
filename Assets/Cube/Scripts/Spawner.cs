using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _scaleRedutionCoefficient;
    [SerializeField] private float _splitChanse = 1f;
    [SerializeField] private float _splitChanseRedutionCoefficient = 2f;
    [SerializeField] private int _minSpawnedCubesCount;
    [SerializeField] private int _maxSpawnedCubesCount;
    [SerializeField] private ExplosionCube _explosionCubePrefab;

    public bool TrySpawn(out List<ExplosionCube> explosionCubes)
    {
        float splitRoll = UnityEngine.Random.value;

        if (splitRoll <= _splitChanse)
        {
            explosionCubes = Spawn();
            return true;
        }

        explosionCubes = null;
        return false;
    }

    public void Init(float splitChanse)
    {
        _splitChanse = splitChanse;
    }

    private void OnValidate()
    {
        if (_minSpawnedCubesCount < 0)
            _minSpawnedCubesCount = 0;

        if (_maxSpawnedCubesCount < _minSpawnedCubesCount)
            _maxSpawnedCubesCount = _minSpawnedCubesCount + 1;
    }

    private List<ExplosionCube> Spawn()
    {
        int spawnedCubesCount = UnityEngine.Random.Range(_minSpawnedCubesCount, _maxSpawnedCubesCount + 1);

        List<ExplosionCube> explosionCubes = new List<ExplosionCube>();

        for (int i = 0; i < spawnedCubesCount; i++)
        {
            Vector3 newExplosionCubeLocalScale = transform.localScale / _scaleRedutionCoefficient;
            float newSplitChanse = _splitChanse / _splitChanseRedutionCoefficient;

            ExplosionCube newExplosionCube = Instantiate(_explosionCubePrefab);
            newExplosionCube.Init(newExplosionCubeLocalScale, newSplitChanse);
            explosionCubes.Add(newExplosionCube);
        }

        return explosionCubes;
    }   
}

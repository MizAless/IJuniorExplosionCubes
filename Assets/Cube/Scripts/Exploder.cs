using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    [SerializeField] private Spawner _spawner;

    public void Explode(Vector3 explodePosition, List<ExplosionCube> explosionCubes)
    {
        foreach (var explosionCube in explosionCubes)
            explosionCube.GetRigidbody().AddExplosionForce(_explosionForce, explodePosition, _explosionRadius);
    }
}

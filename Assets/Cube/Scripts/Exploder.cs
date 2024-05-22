using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void Explode(List<ExplosionCube> explosionCubes)
    {
        foreach (var explosionCube in explosionCubes)
            explosionCube.GetRigidbody().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}

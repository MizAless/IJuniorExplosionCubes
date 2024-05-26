using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public float ExplosionRadius => _explosionRadius;
    public float ExplosionForce => _explosionForce;

    public void Init(float explosionRadius, float explosionForce)
    {
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;
    }

    public void Explode(Vector3 explodePosition)
    {
        List<Rigidbody> explodableObjects = GetExplodableObjects(explodePosition);

        foreach (var explodableObject in explodableObjects)
            explodableObject.AddExplosionForce(_explosionForce, explodePosition, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 explodePosition)
    {
        Collider[] hits = Physics.OverlapSphere(explodePosition, _explosionRadius);

        List<Rigidbody> explodableObjects = new();

        foreach (var hit in hits)
            if (hit.attachedRigidbody != null)
                explodableObjects.Add(hit.attachedRigidbody);

        return explodableObjects;
    }
}

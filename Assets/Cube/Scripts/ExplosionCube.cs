using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]  
[RequireComponent(typeof(Renderer))]  
[RequireComponent(typeof(Exploder))]  
[RequireComponent(typeof(Spawner))]  
public class ExplosionCube : MonoBehaviour
{
    private Exploder _exploder;
    private Spawner _spawner;
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private void Start()
    {
        InitComponents();

        SetRandomColor();
    }

    public Rigidbody GetRigidbody() => _rigidbody;

    public void Init(Vector3 localScale, float splitChanse)
    {
        InitComponents();
        transform.localScale = localScale;
        _spawner.Init(splitChanse);
        SetRandomColor();
    }

    private void InitComponents()
    {
        _exploder = GetComponent<Exploder>();
        _spawner = GetComponent<Spawner>();
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        Destroy();
    }

    private void Destroy()
    {
        if (_spawner.TrySpawn(out List<ExplosionCube> explosionCubes))
            _exploder.Explode(explosionCubes);

        Destroy(gameObject);
    }

    private void SetRandomColor()
    {
        float randomRedValue = UnityEngine.Random.value;
        float randomGreenValue = UnityEngine.Random.value;
        float randomBlueValue = UnityEngine.Random.value;
        float alphaValue = 1f;

        Color newColor = new Color(randomRedValue, randomGreenValue, randomBlueValue, alphaValue);

        string propertyName = "_Color";

        _renderer.material.SetColor(propertyName, newColor);
    }
}

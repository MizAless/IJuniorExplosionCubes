using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]  
[RequireComponent(typeof(Renderer))]  
[RequireComponent(typeof(Exploder))]  

public class ExplosionCube : MonoBehaviour
{
    [SerializeField] private float _splitChance = 1f;

    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Exploder _exploder;

    public float SplitChanse => _splitChance;

    public event Action<ExplosionCube> SplitChanceWorked;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _exploder = GetComponent<Exploder>();

        SetRandomColor();
    }

    private void OnMouseDown()
    {
        Destroy();
    }

    public Rigidbody GetRigidbody() => _rigidbody;

    public Exploder GetExploder() => _exploder;

    public void Init(Vector3 localScale, float splitChanse, float explosionRadius, float explosionForce)
    {
        transform.localScale = localScale;
        _splitChance = splitChanse;
        _exploder.Init(explosionRadius, explosionForce);
        SetRandomColor();
    }

    private void Destroy()
    {
        float splitRoll = UnityEngine.Random.value;

        if (splitRoll <= _splitChance)
        {
            SplitChanceWorked?.Invoke(this);
        }
        else
        {
            _exploder.Explode(transform.position);
        }

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

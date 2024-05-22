using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]  
[RequireComponent(typeof(Renderer))]  

public class ExplosionCube : MonoBehaviour
{
    [SerializeField] private float _splitChance = 1f;

    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public float SplitChanse => _splitChance;

    public event Action<ExplosionCube> Destroyed;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();

        SetRandomColor();
    }

    private void OnMouseDown()
    {
        Destroy();
    }

    public Rigidbody GetRigidbody() => _rigidbody;

    public void Init(Vector3 localScale, float splitChanse)
    {
        transform.localScale = localScale;
        _splitChance = splitChanse;
        SetRandomColor();
    }

    private void Destroy()
    {
        float splitRoll = UnityEngine.Random.value;

        if (splitRoll <= _splitChance)
        {
            Destroyed?.Invoke(this);
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

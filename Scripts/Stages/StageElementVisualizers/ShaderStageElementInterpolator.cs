using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ShaderStageElementInterpolator : StageElementInterpolator<float>
{
    [SerializeField] private string shaderProperty = "_T";
    
    // Internal Variables
    private Material material;

    protected override void Awake()
    {
        base.Awake();
        
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = new Material(renderer.material);
            renderer.material = material;
        }
        else
        {
            Debug.LogError("Renderer component not found on the GameObject.");
        }
    }

    #region Interpolator Functions

    protected override float GetValue()
    {
        return material.GetFloat(shaderProperty);
    }

    protected override void ApplyValue(float value)
    {
        material.SetFloat(shaderProperty, value);
    }

    protected override float LerpValue(float startValue, float endValue, float t)
    {
        return Mathf.Lerp(startValue, endValue, t);
    }
    
    protected override float GetActiveValue()
    {
        return 1;
    }
    
    protected override float GetInactiveValue()
    {
        return 0;
    }

    #endregion
}
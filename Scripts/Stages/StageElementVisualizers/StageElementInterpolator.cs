using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(StageElement))]
public abstract class StageElementInterpolator<T> : MonoBehaviour
{
    [Header("Interpolation Settings")]
    [SerializeField] protected float duration = 0.5f;
    [SerializeField] protected AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    // Internal Variables
    protected Coroutine transitionCoroutine;

    protected virtual void Awake()
    {
        StageElement stageElement = GetComponent<StageElement>();
        stageElement.OnStateUpdated += OnStateUpdated;
    }
    
    protected void OnStateUpdated(bool isActive)
    {
        Debug.Log("OnStateUpdated");
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);
        
        transitionCoroutine = StartCoroutine(Interpolate(isActive));
    }
    
    protected IEnumerator Interpolate(bool isActive)
    {
        float elapsedTime = 0;
        T startValue = GetValue();
        T endValue = isActive ? GetActiveValue() : GetInactiveValue();
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = transitionCurve.Evaluate(elapsedTime / duration);
            ApplyValue(LerpValue(startValue, endValue, t));
            yield return null;
        }
        
        ApplyValue(endValue);
    }
    
    protected abstract T GetValue();
    protected abstract void ApplyValue(T value);
    protected abstract T LerpValue(T startValue, T endValue, float t);
    protected abstract T GetActiveValue();
    protected abstract T GetInactiveValue();
}
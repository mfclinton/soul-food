using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Monster))]
public class MonsterVisualizer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer monsterSpriteRenderer;
    [SerializeField] private SpriteRenderer healthBarFill;
    
    [Header("Flash Properties")]
    [SerializeField] private float flashDuration = 0.1f;
    
    [Header("Shake Properties")]
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeMagnitude = 0.1f;
    
    [Header("Health Bar Properties")]
    [SerializeField] private float healthBarTimePerTick = .1f;
    [SerializeField] private AnimationCurve healthBarFillCurve = AnimationCurve.Linear(0, 0, 1, 1);
    
    // Shader Properties
    private static readonly int MatFill = Shader.PropertyToID("_T");
    
    // Animator Properties
    private static readonly int IsDead = Animator.StringToHash("Dead");
    
    // Internal Variables
    private Coroutine flashRoutine;
    private Coroutine shakeRoutine;
    private Coroutine healthBarRoutine;
    
    // Internal References
    private Monster monster;
    private Animator animator;

    private void Awake()
    {
        monster = GetComponent<Monster>();
        animator = GetComponent<Animator>();
        
        // Copy the material to prevent changing the original
        monsterSpriteRenderer.material = new Material(monsterSpriteRenderer.material);
        
        // Events
        monster.OnInitialized += Initialize;
        monster.OnHealthModified += OnHealthModified;
        monster.OnDeath += OnDeath;
    }

    private void Initialize(MonsterData data)
    {
        monsterSpriteRenderer.sprite = data.monsterSprite;
        animator.runtimeAnimatorController = data.animatorController;
    }

    private void OnDeath()
    {
        StopDamageCoroutines();
        MonsterControllerVisualizer.Instance.DeathPoof(transform.position);
    }
    
    #region Health Changed Event Handling

    private void StopDamageCoroutines()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);
        
        monsterSpriteRenderer.color = Color.white;
        // TODO: fix position
    }
    
    private void StartDamageCoroutines()
    {
        StopDamageCoroutines();
        
        flashRoutine = StartCoroutine(FlashRed());
        shakeRoutine = StartCoroutine(Shake());
    }
    
    private void OnHealthModified(float newHP, float oldHP)
    {
        if (newHP < oldHP)
            StartDamageCoroutines();
        
        if (healthBarRoutine != null)
            StopCoroutine(healthBarRoutine);
        
        healthBarRoutine = StartCoroutine(UpdateHealthBar(newHP, oldHP, monster.MonsterData.monsterHP));
        animator.SetBool(IsDead, monster.IsDead);
    }

    #endregion

    #region Visual Effect Coroutines

    private IEnumerator FlashRed()
    {
        monsterSpriteRenderer.color = Color.red;  
        yield return new WaitForSeconds(flashDuration); 
        monsterSpriteRenderer.color = Color.white;  
    }

    private IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;

        for (float elapsed = 0; elapsed < shakeDuration; elapsed += Time.deltaTime)
        {
            float x = originalPos.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalPos.y + Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = new Vector3(x, y, originalPos.z);
            yield return null;  
        }

        transform.position = originalPos;  
    }
    
    // Set the health bar material fill, Interp it
    private IEnumerator UpdateHealthBar(float newHP, float oldHP, float maxHP)
    {
        float fromT = oldHP / maxHP;
        float toT = newHP / maxHP;
        
        float deltaT = Mathf.Abs(fromT - toT);
        float duration = healthBarTimePerTick * deltaT;
        
        float elapsed = 0;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float curveInterp = healthBarFillCurve.Evaluate(t);
            float fill = Mathf.Lerp(fromT, toT, curveInterp);
            
            healthBarFill.material.SetFloat(MatFill, fill);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    #endregion
}

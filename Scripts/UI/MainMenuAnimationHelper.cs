using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuAnimationHelper : MonoBehaviour
 {
    [Header("Pentagram Circle Drawing")]
    [SerializeField] private Image pentagramCircleImage;
    [SerializeField] private float pentagramCircleDrawDuration = 2f;
    [SerializeField] private AnimationCurve pentagramCircleDrawCurve = AnimationCurve.Linear(0, 0, 1, 1);
    
    [Header("Pentagram Star Drawing")]
    [SerializeField] private Image pentagramStarImage;
    [SerializeField] private float pentagramStarDrawDuration = 2f;
    [SerializeField] private AnimationCurve pentagramStarDrawCurve = AnimationCurve.Linear(0, 0, 1, 1);
    
    [Header("Poof Animation")]
    [SerializeField] private Image poofImage;
    
    [Header("Pentagram SFX")]
    [SerializeField] private AudioSource pentagramDrawSoundSource;
    [SerializeField] private AudioSource poofSoundSource;
    
    // Internal References
    private Material pentagramCircleMaterial;
    private Material pentagramStarMaterial;
    
    // Internal Variables
    private Coroutine pentagramCircleDrawRoutine;
    private Coroutine pentaStarDrawRoutine;
    
    // Animation Constants
    private static readonly int T = Shader.PropertyToID("_T");

    private void Awake()
    {
        // Initialize
        pentagramCircleMaterial = new Material(pentagramCircleImage.material);
        pentagramCircleImage.material = pentagramCircleMaterial;
        
        pentagramStarMaterial = new Material(pentagramStarImage.material);
        pentagramStarImage.material = pentagramStarMaterial;
    }

    private void Start()
    {
        poofImage.gameObject.SetActive(true);
        pentagramCircleImage.gameObject.SetActive(true);
        
        pentagramCircleMaterial.SetFloat(T, 0f);
        pentagramStarMaterial.SetFloat(T, 0f);
    }

    private void UpdateT(Material mat, float t)
    {
        mat.SetFloat(T, t);
    }

    #region Pentragram Circle Drawing

    private void TriggerPentagramCircleDraw()
    {
        if (pentagramCircleDrawRoutine != null)
            StopCoroutine(pentagramCircleDrawRoutine);
        
        pentagramCircleDrawRoutine = StartCoroutine(AnimateCirclePentagram());
    }

    private IEnumerator AnimateCirclePentagram()
    {
        float timeElapsed = 0f;
        while (timeElapsed < pentagramCircleDrawDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = pentagramCircleDrawCurve.Evaluate(timeElapsed / pentagramCircleDrawDuration);
            UpdateT(pentagramCircleMaterial, t);
            yield return null;
        }
    }

    #endregion
     
    #region Pentagram Star Drawing
    
    private void TriggerPentagramStarDraw()
    {
        if (pentaStarDrawRoutine != null)
            StopCoroutine(pentaStarDrawRoutine);
        
        pentaStarDrawRoutine = StartCoroutine(AnimateStarPentagram());
    }
    
    private IEnumerator AnimateStarPentagram()
    {
        float timeElapsed = 0f;
        while (timeElapsed < pentagramStarDrawDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = pentagramStarDrawCurve.Evaluate(timeElapsed / pentagramStarDrawDuration);
            UpdateT(pentagramStarMaterial, t);
            yield return null;
        }
    }
    
    #endregion

    #region Sounds
    
    public void PlayPentagramDrawSound()
    {
        pentagramDrawSoundSource.Play();
    }
    
    public void StopPentaGramDrawSound()
    {
        pentagramDrawSoundSource.Stop();
    }
    
    #endregion
 }

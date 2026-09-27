using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include the TextMesh Pro namespace
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(StageElement))]
public class StageElementSetActive : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    [Header("Color Settings")]
    [SerializeField] private Color inactiveColor;

    // Internal References
    private StageElement stageElement;
    private List<MaskableGraphic> uiElements;
    private Dictionary<MaskableGraphic, Color> activeColors;

    // Coroutine management
    private Coroutine fadeCoroutine;

    #region Unity Functions

    private void Start()
    {
        stageElement = GetComponent<StageElement>();
        uiElements = new List<MaskableGraphic>();
        
        // Collect Elements
        MaskableGraphic[] maskableGraphics = GetComponents<MaskableGraphic>();
        foreach (var graphic in maskableGraphics)
            uiElements.Add(graphic);
        
        foreach (var graphic in GetComponentsInChildren<MaskableGraphic>())
            uiElements.Add(graphic);
        
        // Events
        stageElement.OnStateUpdated += OnStateUpdated;

        // Initial State
        activeColors = GetCurrentColorDictionary();
        
        foreach (var uiElement in uiElements)
            uiElement.color = inactiveColor;
    }

    #endregion

    #region Event Functions

    private void OnStateUpdated(bool isActive)
    {
        StartFading(isActive);
    }

    #endregion

    #region Helper Functions

    private void StartFading(bool isActive)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeUIElements(isActive));
    }

    private IEnumerator FadeUIElements(bool isActive)
    {
        Dictionary<MaskableGraphic, Color> startColors = GetCurrentColorDictionary();

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = fadeCurve.Evaluate(elapsedTime / fadeDuration);
            foreach (var element in uiElements)
            {
                Color targetColor = isActive ? activeColors[element] : inactiveColor;
                element.color = Color.Lerp(startColors[element], targetColor, t);
            }

            yield return null;
        }
    }
    
    private Dictionary<MaskableGraphic, Color> GetCurrentColorDictionary()
    {
        Dictionary<MaskableGraphic, Color> colorDictionary = new Dictionary<MaskableGraphic, Color>();
        foreach (var element in uiElements)
            colorDictionary[element] = element.color;

        return colorDictionary;
    }

    #endregion

}

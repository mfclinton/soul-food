using System.Collections;
using UnityEngine;

public class StageCamera : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private bool freezeZ = true;
    [SerializeField, Range(0, 5f)] private float duration = 1.0f;
    [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    
    // Internal References
    private Coroutine moveCameraCoroutine;
    
    private void Awake()
    {
        StageController stageController = FindAnyObjectByType<StageController>();
        stageController.OnStageChanged += OnStageChanged;
    }

    private void OnStageChanged(Stage oldStage, Stage newStage)
    {
        Transform cameraPosTransform = newStage.CameraPosTransform;
        if(cameraPosTransform == null)
            return;
        
        Vector3 targetPosition = cameraPosTransform.position;
        if(freezeZ)
            targetPosition.z = transform.position.z;
        
        TriggerMoveCamera(targetPosition);
    }
    
    private void TriggerMoveCamera(Vector3 targetPosition)
    {
        if(moveCameraCoroutine != null)
            StopCoroutine(moveCameraCoroutine);
        
        moveCameraCoroutine = StartCoroutine(MoveCamera(targetPosition));
    }
    
    private IEnumerator MoveCamera(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        
        float time = 0.0f;
        while (time < duration)
        {
            float t = curve.Evaluate(time / duration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }
        
        transform.position = targetPosition;
    }
}

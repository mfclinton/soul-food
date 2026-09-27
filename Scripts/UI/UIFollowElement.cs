using UnityEngine;

public class UIFollowElement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float timeToReachTarget = 1f;
    [SerializeField] private AnimationCurve movementCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    // Internal References
    private Vector3 startPosition;
    private float timer = 0f;
    private bool isFollowing = false;

    private void Update()
    {
        if (target != null)
            FollowTarget();
    }

    private void FollowTarget()
    {
        if (!isFollowing)
        {
            startPosition = transform.position;
            timer = 0f;
            isFollowing = true;
        }

        timer += Time.deltaTime;
        float normalizedTime = timer / timeToReachTarget;

        if (normalizedTime >= 1f)
        {
            normalizedTime = 1f;
            isFollowing = false;
        }

        float curveValue = movementCurve.Evaluate(normalizedTime);
        transform.position = Vector3.Lerp(startPosition, target.position, curveValue);
    }

    public void Configure(Transform followTarget)
    {
        target = followTarget;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerInspectableObject : MonoBehaviour
{
    private StateMachine stateMachine;
    private Vector3 startLocalPosition;
    [SerializeField] private AnimationCurve transitionCurve;
    [SerializeField] private float offsetTransitionTime = 0.5f;
    [SerializeField] private float localPositionZOffset = -0.018f;
    // Start is called before the first frame update
    void Start()
    {
        startLocalPosition = transform.localPosition;
        stateMachine = FindObjectOfType<StateMachine>();
    }

	private void OnMouseDown()
	{
        if(stateMachine.GetCurrentState() == StateMachine.State.Inspection)
		{
            StartCoroutine(LerpOffset());
        }
	}

    private IEnumerator LerpOffset()
	{
        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, localPositionZOffset);
        float time = 0f;
        while(time <= offsetTransitionTime)
		{
            gameObject.transform.localPosition = Vector3.Slerp(startPosition, endPosition, transitionCurve.Evaluate(time/offsetTransitionTime));
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPosition;
    }

    public void ResetTransform()
	{
        StopAllCoroutines();
        transform.localPosition = startLocalPosition;
	}
}

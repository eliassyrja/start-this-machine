using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerInspectableObject : MonoBehaviour
{
    private StateMachine stateMachine;
    private Vector3 startLocalPosition;

    [SerializeField] private AnimationCurve transitionCurve;
    [SerializeField] private float offsetTransitionTime = 0.5f;
    [SerializeField] private float localPositionOffset = -0.018f;
    private enum Axis { X, Y, Z};
    [SerializeField] private Axis offsetAxis;

    private void Start()
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
        Vector3 endPosition;

        switch (offsetAxis)
		{
			case Axis.X:
                endPosition = new Vector3(localPositionOffset, transform.localPosition.y, transform.localPosition.z);
                break;
			case Axis.Y:
                endPosition = new Vector3(transform.localPosition.x, localPositionOffset, transform.localPosition.z);
                break;
			case Axis.Z:
                endPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, localPositionOffset);
                break;
			default:
                endPosition = new Vector3(0f, 0f, 0f);
				break;
		}
		
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

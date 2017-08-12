using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RepositionAnimation : MonoBehaviour
{
	public Vector3[] Positions;
	public float AnimationTime = .5f;
	public bool SmoothAnimation = true;
	public float AnimationDelay;
	public UnityEvent OnTargetReached;
	private Vector3 _initPosition;
	public void Awake()
	{
		_initPosition = transform.position;
	}

	public void GoToInitPosition()
	{
		GoToPosition(_initPosition, AnimationTime, SmoothAnimation, OnTargetReached);
	}

	public void GoToPosition(int index)
	{
		GoToPosition(Positions[index], AnimationTime, SmoothAnimation, OnTargetReached);
	}

	public void GoToPosition(Vector3 position)
	{
		GoToPosition(position, AnimationTime, SmoothAnimation, OnTargetReached);
	}

	public void MoveX(float delta)
	{
		Move(Vector3.right * delta);
	}

	public void MoveY(float delta)
	{
		Move(Vector3.up * delta);
	}

	public void MoveZ(float delta)
	{
		Move(Vector3.forward * delta);
	}

	public void Move(Vector3 delta)
	{
		GoToPosition(transform.position + delta, 
			AnimationTime, SmoothAnimation, OnTargetReached);
	}

	public void GoToPosition(Vector3 targetPosition, float duration, 
		bool smoothAnimation, UnityEvent onTargetReached)
	{
		StopAllCoroutines();
		StartCoroutine(LerpToPositionCoroutine(targetPosition, duration, 
			smoothAnimation, AnimationDelay, onTargetReached ));
	}

	public void ForceStopAnimation()
	{
		StopAllCoroutines();
	}

	private IEnumerator LerpToPositionCoroutine(Vector3 targetPosition,
												float duration, bool smoothAnimation, float delay,
												UnityEvent onFinish = null)
	{
		Vector3 ogPosition = transform.position;
		yield return new WaitForSeconds(delay);
		for (float i = 0; i < duration; i += Time.deltaTime)
		{
			transform.position = Vector3.Lerp(
				ogPosition, targetPosition, 
				smoothAnimation ? Mathf.SmoothStep(0f, 1f, i/duration) :
				i/duration);
			yield return 0;
		}
		transform.position = targetPosition;
		if(onFinish != null)
		{
			onFinish.Invoke();
		}
	}
}

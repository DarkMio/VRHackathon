using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RepositionAnimation))]
public class RoomTile : MonoBehaviour
{
	public Vector3 HideOffset = new Vector3(1, 0, -.05f);
	public float AnimationDuration = 1f;

	private RepositionAnimation _animHandler;

	public UnityEvent OnHide, OnShow;
	

	private void Awake()
	{
		_animHandler = GetComponent<RepositionAnimation>();
		//HideForSeconds(5);
	}

	public void Hide()
	{
		//Vector3 hiddenPosition = transform.localPosition + LocalHideOffset;
		_animHandler.GoToPosition(transform.position +  HideOffset, 
			AnimationDuration, false, OnHide);
	}

	public void Show()
	{
		_animHandler.GoToInitPosition();
	}

	public void HideForSeconds(float seconds)
	{
		StopAllCoroutines();
		StartCoroutine(HideForSecondsCoroutine( seconds + AnimationDuration * 2 ));
	}

	private IEnumerator HideForSecondsCoroutine(float seconds)
	{
		Hide();
		yield return new WaitForSeconds(seconds);
		Show();
	}

}

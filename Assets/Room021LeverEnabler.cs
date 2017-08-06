using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room021LeverEnabler : MonoBehaviour {

	[Range(0, 1.1f)]
	public float enabledState;
	[Range(0, 0.25f)]
	public float decayFactor;

	[Range(0, -1.5f)]
	public float yAxisOffset;
	float originalYAxis;

	// Use this for initialization
	void OnEnable () {
		enabledState = 0f;
		originalYAxis = transform.localPosition.y;
	}
	
	// Update is called once per framete
	void Update () {
		SetTransform();
	}

	public void SetTransform() {
		var value = Mathf.SmoothStep(yAxisOffset, originalYAxis, enabledState);
		transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
		enabledState -= decayFactor;
		enabledState = Mathf.Max(0.0f, enabledState);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room021TileRotator : MonoBehaviour {

	[Range(0, 1)]
	public float enabledState;

	[Range(-180f, 180f)]
	public float rotationalOffset;
	float originalRotation;

	// Use this for initialization
	void OnEnable () {
		enabledState = 0f;
		originalRotation = transform.rotation.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
		var value = Mathf.Lerp(originalRotation - rotationalOffset, originalRotation, enabledState);
		transform.rotation = Quaternion.Euler(value, 0, 0);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceRotator : MonoBehaviour {

	public GameObject headset;

	[Range(0, 10)]
	public float minDistance;
	[Range(0, 10)]
	public float maxDistance;
	public float rotationalAmount;

	bool positiveRotation;
	Quaternion quat;


	Vector2 position {
		get {
			return new Vector2(transform.position.x, transform.position.z);
		}
	}

	// Use this for initialization
	void Start () {
		headset = GameObject.FindGameObjectWithTag("MainCamera");
		quat = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
	}
	
	// Update is called once per frame
	void Update () {
		var headsetPos = new Vector2(headset.transform.position.x, headset.transform.position.z);
		var distance = Vector2.Distance(position, headsetPos);
		var fraction = (distance - minDistance) / (maxDistance - minDistance);
		var actuation = Mathf.SmoothStep(0, 1, fraction);

		transform.rotation = Quaternion.AngleAxis(rotationalAmount * actuation, Vector3.up) * quat;

	}
}

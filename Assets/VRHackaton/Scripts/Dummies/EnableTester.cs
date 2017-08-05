using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log(Time.realtimeSinceStartup + ": Test Object Start called.");
	}

	void OnEnable() {
		Debug.Log(Time.realtimeSinceStartup + ": Test Object OnEnable called.");
	}
}

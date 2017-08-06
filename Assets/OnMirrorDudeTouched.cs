using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMirrorDudeTouched : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		if(col.GetComponent<MirrorDudeController>()){
			gameObject.SetActive(false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : ThrowableObject {
    private Vector3 originalPos;
	public GameObject brokenEgg;

    // Use this for initialization
    void Start () {
		
	}

	void Awake() {
		originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		if(col.GetComponent<MirrorDudeController>() == null) {
			if(col.transform.gameObject.name == "Bratpfanne") {
				Debug.Log("WIIIIIN!!!");
			}
			else
				Instantiate(brokenEgg, transform.position, Quaternion.identity, startParent);
				transform.position = originalPos;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : MonoBehaviour {

	public List<GameObject> toEnable;
	[Range(0, 10)]
	public float timeToEnable;
	
	[Tooltip("Time in between each object is enabled - to stagger enable.")]
	[Range(0, 10)]
	public float waitTime;

	void Awake() {
		foreach(var obj in toEnable) {
			obj.SetActive(false);
		}
	}

	void Start () {
		StartCoroutine(EnableGameObject());	
	}

	IEnumerator EnableGameObject() {
		yield return new WaitForSeconds(timeToEnable);
		foreach(var obj in toEnable) {
			obj.SetActive(true);
			yield return new WaitForSeconds(waitTime);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

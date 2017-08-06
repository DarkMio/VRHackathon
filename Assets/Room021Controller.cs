using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Room021Controller : MonoBehaviour {

	public VRTK.VRTK_InteractableObject firstLever;
	public VRTK.VRTK_InteractableObject secondLever;
	public VRTK.VRTK_InteractableObject thirdLever;

	public Room021LeverEnabler slidingLever;

	public List<Light> lights;

	int currentState;
	float initialIntensity;

	[Range(0, 20)]
	public int failureBlink;
	[Range(0, 2)]
	public float failureTime;

	public Color goodSpot;
	public Color badSpot;

	void Start () {
		currentState = 1;
		initialIntensity = lights[0].intensity;
		foreach(var light in lights) {
			light.color = goodSpot;
		}
	}
	
	public void NextStep() {
		slidingLever.enabledState = 1.1f;
		slidingLever.SetTransform();
		slidingLever.enabled = false;
	}

	public void Failure() {
		StartCoroutine(FailureCoreo());
	}

	IEnumerator FailureCoreo() {
		foreach(var light in lights) {
			light.color = badSpot;
		}
		for(int i = 0; i < failureBlink; i++) {
			foreach(var light in lights) {
				light.intensity = initialIntensity - light.intensity;
			}
			yield return new WaitForSeconds(failureTime);
		}
		Start();
	}
}


#if UNITY_EDITOR
[CustomEditor(typeof(Room021Controller))]
public class Room021ControllerEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		var tgt = target as Room021Controller;
		if(GUILayout.Button("Next Step")) {
			tgt.NextStep();
		}
		if(GUILayout.Button("Failure")) {
			tgt.Failure();
		}
	}
}
#endif
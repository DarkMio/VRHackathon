using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelStateController : MonoBehaviour {

	public ClockType clockType;

	List<Light> lights {
		get {
			return new List<Light>{firstSpot, secondSpot, thirdSpot, fourthSpot};
		}
	}

	public Light firstSpot;
	public Light secondSpot;
	public Light thirdSpot;
	public Light fourthSpot;

	List<MeshRenderer> buttons {
		get {
			return new List<MeshRenderer>{firstButton, secondButton, thirdButton, fourthButton};
		}
	}

	public MeshRenderer firstButton;
	public MeshRenderer secondButton;
	public MeshRenderer thirdButton;
	public MeshRenderer fourthButton;

	List<VRTK.VRTK_Button> vrtkButtons {
		get {
			return new List<VRTK.VRTK_Button>{vrtkFirstButton, vrtkSecondButton, vrtkThirdButton, vrtkFourthButton};
		}
	}

	public VRTK.VRTK_Button vrtkFirstButton;
	public VRTK.VRTK_Button vrtkSecondButton;
	public VRTK.VRTK_Button vrtkThirdButton;
	public VRTK.VRTK_Button vrtkFourthButton;

	int currentState;
	float spotIntensity;
	float timeLeft;
	bool countdownRunning;

	public Color goodSpot;
	public Color badSpot;

	public Color goodButton;
	public Color activeButton;
	public Color disabledButton;

	public float time;

	public float blinkRate;
	public int blinkTimes;

    public UnityEvent NextEvent, FailureEvent;

	void Start () {
		countdownRunning = false;
		currentState = 1;
		timeLeft = 0f;
		spotIntensity = firstSpot.intensity;
		foreach(var spot in lights) {
			spot.enabled = false;
		}
		firstSpot.enabled = true;
		firstSpot.color = goodSpot;

		foreach(var button in buttons) {
			button.material.color = disabledButton;
		}
		firstButton.material.color = activeButton;

		foreach(var button in vrtkButtons) {
			button.enabled = false;
		}
		vrtkFirstButton.enabled = true;
	}
	
	public void NextStep() {
		timeLeft += time;
		timeLeft = Mathf.Min(timeLeft, 99.9f);
		StartCoroutine(CountDown());
		currentState = Mathf.Max(Mathf.Min(currentState, 4), 0);
		lights[currentState].enabled = true;
		lights[currentState].color = goodSpot;

		buttons[currentState].material.color = activeButton;
		buttons[currentState - 1].material.color = goodButton;
        SetButtonInteraction(
            buttons[currentState - 1].GetComponent<OurButton>(), true);
        SetButtonInteraction(
            buttons[currentState].GetComponent<OurButton>(), true);


        vrtkButtons[currentState].enabled = true;
		currentState ++;
	}


    public void SetButtonInteraction(OurButton button, bool goodButton)
    {
        button.OnPress = goodButton ? NextEvent : FailureEvent;
    }

	public void Failure() {
		StartCoroutine(FailState());
	}

	IEnumerator CountDown() {
		if(countdownRunning) {
			yield break;
		}
		countdownRunning = true;
		while(timeLeft > 0 && countdownRunning) {
			timeLeft -= Time.deltaTime;
			clockType.value = timeLeft;
			yield return new WaitForEndOfFrame();
		}
		clockType.value = 0f;
	}

	IEnumerator FailState() {
		countdownRunning = false;
		foreach(var button in vrtkButtons) {
			button.enabled = false;
		}
		for(int i = 0; i < blinkTimes * 2; i++) {
			foreach(var spot in lights) {
				spot.color = badSpot;
				spot.intensity = spotIntensity - spot.intensity;
			}
			yield return new WaitForSeconds(blinkRate);
		}
		Start();
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(LevelStateController))]
public class LevelStateControllerEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		var tgt = target as LevelStateController;
		if(tgt == null) {
			return;
		}
		if(GUILayout.Button("Next State")) {
			tgt.NextStep();
		}
		if(GUILayout.Button("Failure")) {
			tgt.Failure();
		}
	}
}
#endif
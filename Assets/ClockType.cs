using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockType : MonoBehaviour {

	public float value;

	public TextMesh firstDigit;
	public TextMesh secondDigit;
	public TextMesh thirdDigit;

	void Update () {
		SetText();
	}

	void SetText() {
		var characters = value.ToString("0.0").ToCharArray();
		Debug.Log(value.ToString("0.0"));
		if(characters.Length == 4) {
			firstDigit.text = characters[0].ToString();
			secondDigit.text = characters[1].ToString();
			thirdDigit.text = characters[3].ToString();
		} else {
			firstDigit.text = "0";
			secondDigit.text = characters[0].ToString();
			thirdDigit.text = characters[2].ToString();
		}
	}
}

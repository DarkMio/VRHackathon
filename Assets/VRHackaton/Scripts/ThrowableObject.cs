using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour, IInteractable {
    protected Transform startParent;

    public void Grab(bool value, MirrorDudeController controller)
    {
        if(value) {
			gameObject.transform.parent = controller.transform;
			gameObject.transform.position = controller.transform.position;
		} else {
			gameObject.transform.parent = startParent;
			GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
		}
    }

    public void Press(bool value, MirrorDudeController controller)
    {
        throw new NotImplementedException();
    }

	void Awake() {
		startParent = transform.parent;
	}
}

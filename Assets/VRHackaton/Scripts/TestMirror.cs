using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMirror : MonoBehaviour {

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Transform mirrorHead;
    public Transform mirrorLeftHand;
    public Transform mirrorRightHand;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mirrorHead.position = new Vector3(head.position.x, head.position.y, -head.position.z);
        mirrorLeftHand.position = new Vector3(leftHand.position.x, leftHand.position.y, -leftHand.position.z);
        mirrorRightHand.position = new Vector3(rightHand.position.x, rightHand.position.y, -rightHand.position.z);
    }
}

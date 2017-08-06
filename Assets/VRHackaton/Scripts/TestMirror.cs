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

	public bool invertX, invertY, invertZ;

    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update()
	{

		MirrorPosition(mirrorHead, head);
		MirrorPosition(mirrorLeftHand, leftHand);
		MirrorPosition(mirrorRightHand, rightHand);
		//mirrorHead.position = new Vector3(head.position.x, head.position.y, -head.position.z);
		//mirrorLeftHand.position =
		//	new Vector3(leftHand.position.x, leftHand.position.y, -leftHand.position.z);
		//mirrorRightHand.position =
		//	new Vector3(
		//		invertX ? -rightHand.position.x : rightHand.position.x,
		//		invertY ? -rightHand.position.y + 2 : rightHand.position.y,
		//		invertZ ? -rightHand.position.z : rightHand.position.z);
	}

	private void MirrorPosition(Transform mirror, Transform t)
	{
		mirror.position = new Vector3(
				invertX ? -t.position.x : t.position.x,
				invertY && t != head ? -t.position.y + 2 : t.position.y,
				invertZ ? -t.position.z : t.position.z + 2.5f);
	}
}

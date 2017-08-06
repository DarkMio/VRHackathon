using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarColor : MonoBehaviour {

    public MirroredAvatar avatar;

	// Use this for initialization
	void Awake () {
        GetComponent<MeshRenderer>().material = avatar.GetMaterial();
    }
}

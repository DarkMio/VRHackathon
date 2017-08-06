using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirroredAvatar : MonoBehaviour
{
	//public ControllerHand PlayerLeftHand, PlayerRightHand;

	public Transform 
		PlayerHead, PlayerLeftHand, PlayerRightHand,
		AvatarHead, AvatarRightHand, AvatarLeftHand, AvatarBody;
	public Vector3 MirrorPointPosition = new Vector3(0,0,0);

	public bool MirrorX, MirrorY, MirrorZ;

    public float _bodyOffset;



    //private void Start ()
    //{
    //	if(PlayerLeftHand.Type != ControllerHand.HandType.Left ||
    //		PlayerRightHand.Type != ControllerHand.HandType.Right)
    //	{
    //		Debug.LogError("MirroredAvatar | Start(): \n\tHand Types are weird");
    //	}
    //}

    private void Update ()
	{
        MirrorPosition(AvatarLeftHand, PlayerLeftHand, MirrorX, MirrorY, MirrorZ);
        MirrorPosition(AvatarHead, PlayerHead, MirrorX, false, MirrorZ);
        MirrorPosition(AvatarRightHand, PlayerRightHand, MirrorX, MirrorY, MirrorZ);
        PositionBody();
		//SetMirroredPositionWithCurrentSettings(PlayerLeftHand.transform);
		//SetMirroredPositionWithCurrentSettings(PlayerRightHand.transform);
		//SetMirroredPositionWithCurrentSettings(AvatarHead);
	}

    private void PositionBody()
    {
        AvatarBody.position = AvatarHead.position + new Vector3(_bodyOffset, 0, 0);
        AvatarBody.rotation = new Quaternion(AvatarBody.rotation.x, AvatarHead.rotation.y, AvatarBody.rotation.z, AvatarBody.rotation.w);
    }

    //private void SetMirroredPositionWithCurrentSettings(Transform t)
    //{
    //	//t.position = MirrorPosition(AvatarHead, PlayerHead,);


    //	//t.position = GetMirroredPosition(t.position, MirrorPointPosition ,MirrorX, MirrorY,MirrorZ);
    //}

    //private Vector3 GetMirroredPosition(
    //	Vector3 position, Vector3 mirrorPoint, 
    //	bool mirrorX, bool mirrorY, bool mirrorZ)
    //{
    //	Vector3 newPosition = position + (mirrorPoint - position) * 2;
    //	if (!mirrorX) newPosition.x = position;
    //	if(!mirrorY) newPosition.y = 
    //	newPosition.y = position.y;
    //	return newPosition;
    //}

    private void MirrorPosition(Transform mirror, Transform t,
		bool invertX, bool invertY, bool invertZ)
	{
		mirror.position = new Vector3(
				invertX ? -t.position.x : t.position.x,
				invertY && t != PlayerHead ? -t.position.y + 2 : t.position.y,
				invertZ ? -t.position.z : t.position.z + 2.5f);

        mirror.rotation = new Quaternion(
            invertX ? -t.rotation.x : -t.rotation.x,
            invertY && t != PlayerHead ? -t.rotation.y : t.rotation.y,
            invertZ ? -t.rotation.z: t.rotation.z,
            t.rotation.w);

	}
}

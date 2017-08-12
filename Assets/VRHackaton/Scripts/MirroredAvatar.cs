using UnityEngine;

public class MirroredAvatar : MonoBehaviour
{
	public Transform AvatarHead, AvatarRightHand, AvatarLeftHand, AvatarBody;
	private Transform _playerHead, _playerLeftHand, _playerRightHand;
	public Vector3 MirrorPointPosition = new Vector3(0, 0, 0);

	public bool MirrorLeftHandX, MirrorLeftHandY, MirrorLeftHandZ;
	public bool MirrorRightHandX, MirrorRightHandY, MirrorRightHandZ;
	public bool MirrorHeadX, MirrorHeadY, MirrorHeadZ;

	public Color AvatarStartingColor;

	public Color AvatarColor
	{
		set
		{
			UpdateColor(value);
		}
	}

	public float BodyOffset;

	private void Awake()
	{
		AvatarColor = AvatarStartingColor;
		GameUtil util = GameUtil.Instance;
		_playerHead = util.HeadTransform;
		_playerLeftHand = util.ControllerLeft.transform;
		_playerRightHand = util.ControllerRight.transform;
	}

	private void Update()
	{
		MirrorPosition(AvatarLeftHand, _playerLeftHand, 
			MirrorLeftHandX, MirrorLeftHandY, MirrorLeftHandZ);
		MirrorPosition(AvatarRightHand, _playerRightHand, 
			MirrorRightHandX, MirrorRightHandY, MirrorRightHandZ);
		MirrorPosition(AvatarHead, _playerHead, 
			MirrorHeadX, MirrorHeadY, MirrorHeadZ);
		PositionBody();
	}

	//public void SetType()
	//{
		
	//}
	
	private void PositionBody()
	{
		AvatarBody.position = AvatarHead.position + new Vector3(0, BodyOffset, 0);
		AvatarBody.rotation = new Quaternion(
			AvatarBody.rotation.x, AvatarHead.rotation.y, 
			AvatarBody.rotation.z, AvatarBody.rotation.w);
	}

	private void MirrorPosition(Transform mirror, Transform t,
		bool invertX, bool invertY, bool invertZ)
	{
		mirror.position = new Vector3(
				invertX ? -t.position.x : t.position.x,
				invertY && t != _playerHead ? -t.position.y + 2 : t.position.y,
				invertZ ? -t.position.z : t.position.z + 2.5f);

		mirror.rotation = new Quaternion(
			invertX ? t.rotation.x : -t.rotation.x,
			invertY && t != _playerHead ? -t.rotation.y : t.rotation.y,
			invertZ ? -t.rotation.z : t.rotation.z,
			t.rotation.w);

	}

	private void UpdateColor(Color color)
	{
		foreach (Renderer r in GetComponentsInChildren<Renderer>())
		{
			r.material.color = color;
		}
	}

}

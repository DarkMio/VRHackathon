using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class RoomManager : MonoBehaviour {

	[Range(0, 25)]
	public float totalRotations;
	[Range(0, 25)]
	public float animationDuration;
	public List<Transform> rooms;

	[Tooltip("Will later be the state machine of the screen animation controller")]
	public MonoBehaviour screenSlider; 

	public Transform spawnReference;
	public Transform roomReference;
	public Transform outReference;


	public delegate void TransitionCallback();
	public TransitionCallback roomTransitionBegin;
	public TransitionCallback roomTransitionEnd;
	public TransitionCallback playlistEnd;

	
	private int currentRoom;
	private bool animating;

	void Start () {
		InitRooms();
	}
	


	public void NextRoom() {
		if(animating) {
			Debug.LogWarning("Currently busy animating.");
			return;
		}

		if(rooms.Count <= currentRoom) {
			Debug.LogWarning("Depleted all rooms from playlist.");
			return;
		}

		animating = true;
		StartCoroutine(MovementAnimation());

	}

	IEnumerator MovementAnimation() {
		if(roomTransitionBegin != null) {
			roomTransitionBegin();
		}

		var outTransform = rooms[currentRoom];
		var inTransform = rooms[currentRoom + 1];

		float innerDeltaTime = Time.deltaTime;
		while(innerDeltaTime < animationDuration) {
			float interpolFrac = innerDeltaTime / animationDuration;
			float inverseFrac = 1 - interpolFrac;

			inTransform.rotation = Quaternion.AngleAxis(-360 * totalRotations * Mathf.Pow(inverseFrac, 3), Vector3.up);
			inTransform.position = Vector3.Lerp(spawnReference.position, roomReference.position, interpolFrac);
			inTransform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), roomReference.localScale, interpolFrac);

			outTransform.rotation = Quaternion.AngleAxis(360 * totalRotations * Mathf.Pow(interpolFrac, 3), Vector3.up);
			outTransform.position = Vector3.Lerp(roomReference.position, outReference.position, interpolFrac);
			outTransform.localScale = Vector3.Lerp(roomReference.localScale, new Vector3(0, 0, 0), interpolFrac);

			innerDeltaTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		currentRoom++;
		animating = false;
		if(rooms.Count <= currentRoom) {
			if(playlistEnd != null) {
				playlistEnd();
			}
		}
	}

	void InitRooms() {
		currentRoom = 0;

		if(rooms.Count < 1) {
			Debug.LogWarning("No rooms in playlist.");
			if(playlistEnd != null) {
				playlistEnd();
			}
			return;
		}



		// hide all other rooms but one - move them to the spawn pos, too
		for(int i = 1; i < rooms.Count; i++) {
			rooms[i].localScale = new Vector3(0, 0, 0);
			rooms[i].position = spawnReference.position;
		}

		rooms[0].position = roomReference.position;
		rooms[0].rotation = roomReference.rotation;
		rooms[0].localScale = new Vector3(0, 0, 0);
	}
}


# if UNITY_EDITOR

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		if(GUILayout.Button("Next Room")) {
			var tgt = target as RoomManager;
			if(tgt != null) {
				tgt.NextRoom();
			}
		}
	}

}

# endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour {
	public static GlobalVars current;

	public int currentRoomID = 1;
	public string currentRoomName = "Room Name";
	public Vector2 currentRoomSize;
	public Vector2 currentRoomPosition;
	public string currentDialogue;

	void Awake() {
		current = this;
	}

}

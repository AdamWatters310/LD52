using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInit : MonoBehaviour {

	public Vector2 roomSize;
	private Vector2 roomPosition;
	public string roomName;

	private void Start() {
		roomPosition = transform.position;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")){
			GlobalVars.current.currentRoomPosition = roomPosition;
			GlobalVars.current.currentRoomSize = roomSize;
			GlobalVars.current.currentRoomName = roomName;
			GameEvents.current.RoomChange();
			GameEvents.current.RoomNameChange();
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(roomPosition, roomSize);
	}
}

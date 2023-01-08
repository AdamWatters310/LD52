using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInit : MonoBehaviour {

	public Vector2 roomSize;
	private Vector2 roomPosition;
	public string roomName;
	public GameObject roomPrefab;
	GameObject InstantiatedParentObject;

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
			if (roomPrefab != null)
				InstantiatedParentObject = Instantiate(roomPrefab);
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if(collision.CompareTag("Player") && InstantiatedParentObject != null) {
			Destroy(InstantiatedParentObject);
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(roomPosition, roomSize);
	}
}

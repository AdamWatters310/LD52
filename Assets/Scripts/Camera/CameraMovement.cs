using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Camera cam;
	public Controller target;
	public Vector2 focusAreaSize;
	FocusArea focus = new FocusArea();
	public float verticalOffset;

	public Vector2 RoomSize;

	public float lookAheadDistX;
	public float smoothTimeX;
	public float VerticalSmoothTime;

	float currentLookX;
	float targetLookX;
	float directionLookX;
	float smoothLookVelX;
	float smoothVelY;

	GlobalVars var;

	public Vector2 roomcenter;

	bool lookAheadStopped;

	public static CameraMovement current;


	private void Awake() {
		current = this;
	}

	void Start() {
		focus = new FocusArea(target.collider.bounds, focusAreaSize);
		var = GlobalVars.current;
		GameEvents.current.OnRoomChange += OnUpdateRoomData;
	}

	private void LateUpdate() {
		focus.Update(target.collider.bounds);
		Vector2 focusPosition = focus.center + Vector2.up * verticalOffset;

		if (focus.velocity.x != 0) {
			directionLookX = Mathf.Sign(focus.velocity.x);
			if (Mathf.Sign(Input.GetAxis("Horizontal")) == Mathf.Sign(focus.velocity.x) && Input.GetAxis("Horizontal") != 0) {
				lookAheadStopped = false;
				targetLookX = directionLookX * lookAheadDistX;
			} else {
				if (!lookAheadStopped) {
					lookAheadStopped = true;
					targetLookX = currentLookX + (directionLookX * lookAheadDistX - currentLookX) / 4f;
				}
			}
		}

		currentLookX = Mathf.SmoothDamp(currentLookX, targetLookX, ref smoothLookVelX, smoothTimeX);

		focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelY, VerticalSmoothTime);
		focusPosition += Vector2.right * currentLookX;

		focusPosition.x = Mathf.Clamp(focusPosition.x, roomcenter.x - RoomSize.x / 2f + cam.orthographicSize * 16 / 9, roomcenter.x + current.RoomSize.x / 2f - current.cam.orthographicSize * 16 / 9);
		focusPosition.y = Mathf.Clamp(focusPosition.y, roomcenter.y - RoomSize.y / 2f + cam.orthographicSize, roomcenter.y + RoomSize.y / 2f - cam.orthographicSize);

		transform.position = (Vector3)focusPosition + Vector3.forward * -10;
	}

	[System.Serializable]
	struct FocusArea {
		public Vector2 center;
		public float left, right, top, bottom;
		public Vector2 velocity;
		public FocusArea(Bounds targetBounds, Vector2 size) {
			left = targetBounds.center.x - size.x / 2;
			right = targetBounds.center.x + size.x / 2;
			bottom = targetBounds.min.y;
			top = targetBounds.min.y + size.y;

			velocity = Vector2.zero;
			center = new Vector2((left + right) / 2, (top + bottom) / 2);
		}
		public void Update(Bounds targetBounds) {
			float shiftx = 0;
			if (targetBounds.min.x < left)
				shiftx = targetBounds.min.x - left;
			else if (targetBounds.max.x > right)
				shiftx = targetBounds.max.x - right;

			//			Debug.Log("TargetBoundsX: " + targetBounds.min.x +", " + targetBounds.max.x);
			//			Debug.Log("TargetBoundsY: " + targetBounds.min.y +", " + targetBounds.max.y);


			left += shiftx;
			right += shiftx;

			float shifty = 0;
			if (targetBounds.min.y < bottom)
				shifty = targetBounds.min.y - bottom;
			else if (targetBounds.max.y > top)
				shifty = targetBounds.max.y - top;

			top += shifty;
			bottom += shifty;
			center = new Vector2((left + right) / 2, (top + bottom) / 2);
			center.x = Mathf.Clamp(center.x, current.roomcenter.x - current.RoomSize.x / 2f, current.roomcenter.x + current.RoomSize.x / 2f - current.cam.orthographicSize * 16 / 9);
			center.y = Mathf.Clamp(center.y, current.roomcenter.y - current.RoomSize.y / 2f, current.roomcenter.y + current.RoomSize.y / 2f - current.cam.orthographicSize);
			velocity = new Vector2(shiftx, shifty);
		}
	}

	public void OnUpdateRoomData() {
		Debug.Log("Updated Room Data");
		RoomSize = var.currentRoomSize;
		roomcenter = var.currentRoomPosition;
	}

	/*	private void OnDrawGizmos() {
			Gizmos.color = new Color(0, 255, 255);
			for(int i = 0; i < 10; i++) {
				for(int j = 0; j > -9; j--) {
					Gizmos.DrawWireCube(new Vector3(i * 32f + 16f, j * 18f + 9f, 1f), new Vector3(32f, 18f, 2f));
				}
			}
			Gizmos.color = new Color(1, 0, 0, .5f);
			Gizmos.DrawCube(focus.center, focusAreaSize);

			Gizmos.color = new Color(0, 1, 0);
			Gizmos.DrawWireSphere(roomcenter, 1f);

		}
	*/
}
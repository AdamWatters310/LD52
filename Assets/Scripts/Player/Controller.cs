using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : RaycastController
{
	public CollisionInfo collisions;
	public LayerMask collisionLayer;
	public LayerMask fullCollisions;
	public LayerMask lowerCollisions;

	private void Start() {
		CalculateRaySpacing();

		GameEvents.current.OnWalkThroughWallsEnable += OnThinWallWalk;
		GameEvents.current.OnWalkThroughWallsDisable += removeOnThingWallWalk;
	}

	public void Move(Vector3 moveDistance) {
		collisions.Reset();
		UpdateRaycastOrigins();
		if (moveDistance.x != 0)
			HorizontalColissions(ref moveDistance);
		if (moveDistance.y != 0)
			VerticalColissions(ref moveDistance);
		//	PlayerAnims.current.animatePlayer(moveDistance);
		transform.Translate(moveDistance);
	}

	void HorizontalColissions(ref Vector3 moveDistance) {
		float directionX = Mathf.Sign(moveDistance.x);
		float rayLength = Mathf.Abs(moveDistance.x) + skinWidth;
		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionLayer);
			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

			if (hit) {
				if (hit.distance == 0f) continue;
				moveDistance.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
			}
		}
	}

	void VerticalColissions(ref Vector3 moveDistance) {
		float directionY = Mathf.Sign(moveDistance.y);
		float rayLength = Mathf.Abs(moveDistance.y) + skinWidth;
		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + moveDistance.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionLayer);
			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

			if (hit) {
				moveDistance.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;
				collisions.above = directionY == 1;
				collisions.below = directionY == -1;
			}
		}
	}

	[System.Serializable]
	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;
		public void Reset() {
			above = below = left = right = false;
		}
	}

	void OnThinWallWalk() {
		collisionLayer = lowerCollisions;
	}

	void removeOnThingWallWalk() {
		collisionLayer = fullCollisions;
	}


}

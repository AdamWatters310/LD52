using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEnemies : RaycastController {
	bool collisionActed = false;
	[SerializeField] float gravityScale = 1f;
	[SerializeField] float MaxJumpHeight = 6f;
	[SerializeField] float MinJumpHeight = 1f;
	[SerializeField] float timetoJumpApex = .9f;

	[SerializeField] float gravity = -40f;                      //a, as gravity goes in opposite dirtection to jump motion, it should be a negative value
	[SerializeField] float fallTimer;                //t
	public bool falling;
	public CollisionInfo collisions;
	public LayerMask collisionLayer;
	public float moveSpeed = 2.4f;
	Vector2 movement;

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

				if (collisions.climbing)
					moveDistance.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveDistance.x);

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

				if (collisions.climbing) {
					moveDistance.x = moveDistance.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveDistance.x);
				}

				collisions.above = directionY == 1;
				collisions.below = directionY == -1;
			}
		}
		if (collisions.climbing) {
			float directionX = Mathf.Sign(moveDistance.x);
			rayLength = Mathf.Abs(moveDistance.x) + skinWidth;
			Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * moveDistance.y;
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionLayer);

			if (hit) {
				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
				if (slopeAngle != collisions.slopeAngle) {
					moveDistance.x = (hit.distance - skinWidth) * directionX;
					collisions.slopeAngle = slopeAngle;
				}
			}
		}
	}

	[System.Serializable]
	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;
		public bool climbing, descending;
		public float slopeAngle, prevSlopeAngle;
		public void Reset() {
			above = below = left = right = climbing = descending = false;
			prevSlopeAngle = slopeAngle;
			slopeAngle = 0;
		}
	}

	void Update() {
		if(moveSpeed > 0f)
			transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
		else
			transform.localScale = Vector3.one;
		if (collisions.below) {
			fallTimer = 0f;
		}
		movement = Vector2.zero;
		movement.x = Time.deltaTime * moveSpeed;
		movement.y = (gravity * (.5f + fallTimer) * gravityScale) * Time.deltaTime;
		fallTimer += Time.deltaTime;
		Move(movement);
		if (collisions.right || collisions.left) moveSpeed *= -1;

	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			GameEvents.current.DecreaseHealth();
		}
	}
}
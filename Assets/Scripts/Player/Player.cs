using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	Vector2 movement;
	[SerializeField] float gravityScale = 1f;
	[SerializeField] float MaxJumpHeight = 6f;
	[SerializeField] float MinJumpHeight = 1f;
	[SerializeField] float timetoJumpApex = .9f;

	[SerializeField] float JumpVelStart;                        //u
	[SerializeField] float gravity = -40f;                      //a, as gravity goes in opposite dirtection to jump motion, it should be a negative value
	[SerializeField] float JumpTimer, fallTimer;                //t
	[SerializeField] float JumpedDistance;                      //s, not used for final velocity calculations. Only used if jump button is released early

	[SerializeField] float dashLength = 8f;
	[SerializeField] float dashTime;
	public float moveSpeed = 6f;
	Vector2 direction;

	public float HorizontalInput = 0f;

	bool hitJumpApex;
	public bool jumping, falling;
	bool storedJump;
	public float jumpGracePeriod;
	float jumpGraceDefault;
	float defaultMaxJumpHeight;
	bool isDashing = false;
	
	bool canDash = false;
	bool dashAvailable = false;
	public bool canDoubleJump = false;

	public bool hasSecondJump = false;
	public bool usedSecondJump = false;

	Controller controller;
	public bool facingRight;

	public AudioSource jumpSFX;
	public AudioSource dashSFX;
	public AudioSource shootSFX;

	public static Player current;

	private void Awake() {
		current = this;
	}

	void Start() {
		defaultMaxJumpHeight = MaxJumpHeight;
		jumpGraceDefault = jumpGracePeriod;
		controller = GetComponent<Controller>();
		//JumpVelStart = (float)((MaxJumpHeight / timetoJumpApex) - (0.5 * gravity * timetoJumpApex));

		//Event method subscriptions
		GameEvents.current.OnDashEnable += GetDash;
		GameEvents.current.OnDashDisable += RemoveDash;
		GameEvents.current.OnDoubleJumpEnable += GetDoubleJump;
		GameEvents.current.OnDoubleJumpDisable += RemoveDoubleJump;
		GameEvents.current.OnHighJumpEnable += GetHighJump;
		GameEvents.current.OnHighJumpDisable += RemoveHighJump;

	}

	void Update() {
		if (!isDashing) {
			if (controller.collisions.below) {
				canDash = dashAvailable;
				fallTimer = 0f;
				hasSecondJump = false;
				usedSecondJump = false;
			}
			if(Input.GetKeyDown(KeyCode.C) && canDash) {
				JumpedDistance = 0f;

				isDashing = true;
				StartCoroutine(dashing());
			}
			HorizontalInput = 0f;
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) HorizontalInput += 1f;
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) HorizontalInput -= 1f;
			if (storedJump) {
				jumpGracePeriod -= Time.deltaTime;
				if (jumpGracePeriod <= 0f) {
					storedJump = false;
					jumpGracePeriod = jumpGraceDefault;
				}
			}
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
				storedJump = true;
			movement = Vector2.zero;
			if (storedJump && (controller.collisions.below || hasSecondJump)) {
				Debug.Log("Initiating Jump");
				jumping = true;
				JumpVelStart = Mathf.Sqrt(-2 * gravity * MaxJumpHeight);
				JumpTimer = 0f;
				fallTimer = 0f;
				jumpSFX.Play();
				if (hasSecondJump) {
					usedSecondJump = true;
					hasSecondJump = false;
				}
			}
			if (jumping || storedJump) {
				if (Mathf.Abs(MaxJumpHeight - JumpedDistance) <= 0.4f) hitJumpApex = true;
				JumpTimer += Time.deltaTime;
				movement.y = (JumpVelStart + (gravity * JumpTimer)) * Time.deltaTime;
				JumpedDistance += movement.y;

			}
			if (jumping && (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.W))
					&& !hitJumpApex) {
				if (canDoubleJump && !hasSecondJump) {
					hasSecondJump = true;
					storedJump = false;
				}
				JumpVelStart = Mathf.Sqrt(-2 * gravity * JumpedDistance + .2f);
			}
			if (jumping && controller.collisions.above) {
				jumping = false;
				storedJump = false;
			}
			movement.x = HorizontalInput * Time.deltaTime * moveSpeed;
			if (!jumping) {
				movement.y = (gravity * (.5f + fallTimer) * gravityScale) * Time.deltaTime;
				fallTimer += Time.deltaTime;
			}
			controller.Move(movement);
			if (controller.collisions.below) {
				hitJumpApex = jumping = false;
				JumpTimer = 0f;
				JumpedDistance = 0f;
			}
		} else {
			fallTimer = 0f;
		}

	}

	private IEnumerator dashing() {
		dashSFX.Play();
		canDash = false;
		Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if (direction == Vector2.zero)
			direction = Vector2.right * (facingRight ? 1 : -1);
		float elapsedTime = 0f;
		float moveDistance = 0f;
		while(isDashing) {
			elapsedTime += Time.deltaTime;
			moveDistance = dashLength * (Time.deltaTime / dashTime);

			controller.Move(moveDistance * direction);
			if (elapsedTime >= dashTime || controller.collisions.below || controller.collisions.right || controller.collisions.left || controller.collisions.left)
				break;
			else
				yield return null;
		}
		falling = true;
		isDashing = false;
	}

	void Shoot() {
		if (!isDashing) {
			shootSFX.Play();
		}
	}

	//EVENT METHODS

	void RemoveDoubleJump() {
		canDoubleJump = false;
		hasSecondJump = false;
	}

	void GetDoubleJump() {
		canDoubleJump = true;
	}

	void GetHighJump() {
		MaxJumpHeight = 10f;
	}

	void RemoveHighJump() {
		MaxJumpHeight = defaultMaxJumpHeight;
	}

	void GetDash() {
		dashAvailable = true;
	}

	void RemoveDash() {
		dashAvailable = false;
	}


}
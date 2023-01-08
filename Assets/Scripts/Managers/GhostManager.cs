using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostManager : MonoBehaviour {
	
	int ghostsInUse = 0;
	int maxGhostsAllowed = 2;

	public bool hasDash, hasShoot, hasDJ, hasHJ, hasWallWalk;
	public bool dashEnabled, shootEnabled, DJEnabled, HJEnabled, WallWalkEnabled;

	public Image dashBackground, shootBackground, highJumpBackground, doubleJumpBackground, wallWalkBackground;
	public Image dashPortrait, shootPortrait, highJumpPortrait, doubleJumpPortrait, wallWalkPortrait;

	public bool enableAllUpgradesAtStart = false;

	void Start() {
		GameEvents.current.OnDashEnable += GetDash;
		GameEvents.current.OnDashDisable += RemoveDash;
		GameEvents.current.OnDoubleJumpEnable += GetDoubleJump;
		GameEvents.current.OnDoubleJumpDisable += RemoveDoubleJump;
		GameEvents.current.OnHighJumpEnable += GetHighJump;
		GameEvents.current.OnHighJumpDisable += RemoveHighJump;
		GameEvents.current.OnWalkThroughWallsEnable += GetWallWalk;
		GameEvents.current.OnWalkThroughWallsDisable += RemoveWallWalk;
		GameEvents.current.OnShootEnable += GetShoot;
		GameEvents.current.OnShootDisable += RemoveShoot;

		GameEvents.current.OnDashGet += FindDash;
		GameEvents.current.OnShootGet += FindShoot;
		GameEvents.current.OnHighJumpGet += FindHJ;
		GameEvents.current.OnDoubleJumpGet += FindDJ;
		GameEvents.current.OnWalkThroughWallsGet += FindWallWalk;
		GameEvents.current.OnExtraGhostGet += FindDoubleGhost;

		GameEvents.current.OnGhostTimeout += GhostTimeout;
	}


	// Update is called once per frame
	void Update() {
		if (enableAllUpgradesAtStart) {
			maxGhostsAllowed++;
			GameEvents.current.DashGet();
			GameEvents.current.ShootGet();
			GameEvents.current.HighJumpGet();
			GameEvents.current.DoubleJumpGet();
			GameEvents.current.WalkThroughWallsGet();
			enableAllUpgradesAtStart = false;
		}
		if (Input.GetKeyDown(KeyCode.Alpha1) && hasDash) {
			if (dashEnabled)
				GameEvents.current.DashDisable();
			else if(ghostsInUse < maxGhostsAllowed)
				GameEvents.current.DashEnable();
		}

		if (Input.GetKeyDown(KeyCode.Alpha2) && hasShoot) {
			if (shootEnabled)
				GameEvents.current.ShootDisable();
			else if(ghostsInUse < maxGhostsAllowed)
				GameEvents.current.ShootEnable();
		}

		if (Input.GetKeyDown(KeyCode.Alpha3) && hasHJ) {
			if (HJEnabled)
				GameEvents.current.HighJumpDisable();
			else if (ghostsInUse < maxGhostsAllowed)
				GameEvents.current.HighJumpEnable();
		}

		if (Input.GetKeyDown(KeyCode.Alpha4) && hasWallWalk) {
			if (WallWalkEnabled)
				GameEvents.current.WalkThroughWallsDisable();
			else if (ghostsInUse < maxGhostsAllowed)
				GameEvents.current.WalkThroughWallsEnable();
		}

		if (Input.GetKeyDown(KeyCode.Alpha5) && hasDJ) {
			if (DJEnabled)
				GameEvents.current.DoubleJumpDisable();
			else if (ghostsInUse < maxGhostsAllowed)
				GameEvents.current.DoubleJumpEnable();
		}

	}

	void RemoveDoubleJump() {
		if (DJEnabled) {
			ghostsInUse--;
			DJEnabled = false;
			doubleJumpBackground.color = Color.blue;
			GameEvents.current.GhostDisable();
		}
	}

	void GetDoubleJump() {
		ghostsInUse++;
		DJEnabled = true;
		doubleJumpBackground.color = Color.green;
		GameEvents.current.GhostEnable();
	}

	void GetHighJump() {
		ghostsInUse++;
		HJEnabled = true;
		highJumpBackground.color = Color.green;
		GameEvents.current.GhostEnable();
	}

	void RemoveHighJump() {
		if (HJEnabled) {
			ghostsInUse--;
			HJEnabled = false;
			highJumpBackground.color = Color.blue;
			GameEvents.current.GhostDisable();
		}
	}

	void GetDash() {
		ghostsInUse++;
		dashEnabled = true;
		dashBackground.color = Color.green;
		GameEvents.current.GhostEnable();
	}

	void RemoveDash() {
		if (dashEnabled) {
			ghostsInUse--;
			dashEnabled = false;
			dashBackground.color = Color.blue;
			GameEvents.current.GhostDisable();
		}
	}

	void GetWallWalk() {
		ghostsInUse++;
		WallWalkEnabled = true;
		wallWalkBackground.color = Color.green;
		GameEvents.current.GhostEnable();
	}

	void RemoveWallWalk() {
		if (WallWalkEnabled) {
			ghostsInUse--;
			WallWalkEnabled = false;
			wallWalkBackground.color = Color.blue;
			GameEvents.current.GhostDisable();
		}
	}

	void GetShoot() {
		ghostsInUse++;
		shootEnabled = true;
		shootBackground.color = Color.green;
		GameEvents.current.GhostEnable();
	}

	void RemoveShoot() {
		if (shootEnabled) {
			ghostsInUse--;
			shootEnabled = false;
			shootBackground.color = Color.blue;
			GameEvents.current.GhostDisable();
		}
	}



	void FindDash() {
		hasDash = true;
		dashBackground.color = Color.blue;
		dashPortrait.color = new Color(255f, 255f, 255f, 255f);
	}

	void FindShoot() {
		hasShoot = true;
		shootBackground.color = Color.blue;
		shootPortrait.color = new Color(255f, 255f, 255f, 255f);
	}

	void FindHJ() {
		hasHJ = true;
		highJumpBackground.color = Color.blue;
		highJumpPortrait.color = new Color(255f, 255f, 255f, 255f);
	}

	void FindWallWalk() {
		hasWallWalk = true;
		wallWalkBackground.color = Color.blue;
		wallWalkPortrait.color = new Color(255f, 255f, 255f, 255f);
	}

	void FindDJ() {
		hasDJ = true;
		doubleJumpBackground.color = Color.blue;
		doubleJumpPortrait.color = new Color(255f, 255f, 255f, 255f);
	}

	void FindDoubleGhost() {
		maxGhostsAllowed++;
	}

	void GhostTimeout() {
		ghostsInUse = 0;
		GameEvents.current.DashDisable();
		GameEvents.current.ShootDisable();
		GameEvents.current.WalkThroughWallsDisable();
		GameEvents.current.HighJumpDisable();
		GameEvents.current.DoubleJumpDisable();
	}
}

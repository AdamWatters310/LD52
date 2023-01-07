using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour {

	public static GameEvents current;
	// Start is called before the first frame update
	void Awake() {
		current = this;	
	}

	public event Action OnGhostEnable;
	public event Action OnGhostTimeout;
	public event Action OnGhostDisable;

	public event Action OnWalkThroughWallsEnable;
	public event Action OnWalkThroughWallsDisable;
	public event Action OnShootEnable;
	public event Action OnShootDisable;
	public event Action OnDashEnable;
	public event Action OnDashDisable;
	public event Action OnDoubleJumpEnable;
	public event Action OnDoubleJumpDisable;
	public event Action OnHighJumpEnable;
	public event Action OnHighJumpDisable;

	public event Action OnPause;
	public event Action OnUnpause;

	public event Action OnWalkThroughWallsGet;
	public event Action OnShootGet;
	public event Action OnDashGet;
	public event Action OnDoubleJumpGet;	
	public event Action OnHighJumpGet;
	public event Action OnExtraGhostGet;

	public event Action OnRoomNameChange;
	public event Action OnRoomChange;

	public event Action OnDialogueTrigger;

	public event Action OnHealthUpdated;
	public event Action OnHealthIncrease;
	public event Action OnHealthDecrease;
	public event Action OnHealthBigDecrease;
	public event Action OnRefillHealth;

	public event Action OnGetMoreSoul;

	public event Action OnGameOver;
	public event Action OnReturnToTitle;

	public event Action OnUIHeadTransparent;
	public event Action OnUIHeadOpaque;

	public event Action OnBossDefeat;

	public void GhostEnable() {
		OnGhostEnable?.Invoke();
	}

	public void GhostDisable() {
		OnGhostDisable?.Invoke();
	}

	public void RoomChange() {
		OnRoomChange?.Invoke();
	}

	public void RoomNameChange() {
		OnRoomNameChange?.Invoke();
	}

	public void DashEnable() {
		OnDashEnable?.Invoke();
	}

	public void DashDisable() {
		OnDashDisable?.Invoke();
	}

	public void ShootEnable() {
		OnShootEnable?.Invoke();
	}

	public void ShootDisable() {
		OnShootDisable?.Invoke();
	}

	public void DoubleJumpEnable() {
		OnDoubleJumpEnable?.Invoke();
	}

	public void DoubleJumpDisable() {
		OnDoubleJumpDisable?.Invoke();
	}

	public void HighJumpEnable() {
		OnHighJumpEnable?.Invoke();
	}

	public void HighJumpDisable() {
		OnHighJumpDisable?.Invoke();
	}

	public void WalkThroughWallsEnable() {
		OnWalkThroughWallsEnable?.Invoke();
	}

	public void WalkThroughWallsDisable() {
		OnWalkThroughWallsDisable?.Invoke();
	}

	public void DashGet() {
		OnDashGet?.Invoke();
	}

	public void ShootGet() {
		OnShootGet?.Invoke();
	}

	public void HighJumpGet() {
		OnHighJumpGet?.Invoke();
	}

	public void DoubleJumpGet() {
		OnDoubleJumpGet?.Invoke();
	}

	public void WalkThroughWallsGet() {
		OnWalkThroughWallsGet?.Invoke();
	}

	public void DoubleGhostGet() {
		OnExtraGhostGet?.Invoke();
	}

	public void TriggerDialogue() {
		OnDialogueTrigger?.Invoke();
	}

	public void GetMoreSoul() {
		OnGetMoreSoul?.Invoke();	
	}

	public void MakeUITransparent() {
		OnUIHeadTransparent?.Invoke();
	}

	public void MakeUIOpaque() {
		OnUIHeadOpaque?.Invoke();
	}

	public void GhostTimeout() {
		OnGhostTimeout?.Invoke();
	}

	public void DecreaseHealth() {
		OnHealthDecrease?.Invoke();
	}
}

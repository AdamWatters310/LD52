using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravestone : MonoBehaviour {
	public enum Upgrades {
		Dash,
		WallWalk,
		Shoot,
		DoubleJump,
		DoubleGhost,
		HighJump
	}
	public Upgrades addUpgrade;

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.CompareTag("Player")) {
			Debug.Log("Collided with player");
			GameEvents.current.TriggerDialogue();
			switch (addUpgrade) {
				case Upgrades.Dash:
					GameEvents.current.DashGet();
					break;
				case Upgrades.DoubleGhost:
					GameEvents.current.DoubleGhostGet();
					break;
				case Upgrades.DoubleJump:
					GameEvents.current.DoubleJumpGet();
					break;
				case Upgrades.Shoot:
					GameEvents.current.ShootGet();
					break;
				case Upgrades.WallWalk:
					GameEvents.current.WalkThroughWallsGet();
					break;
				case Upgrades.HighJump:
					GameEvents.current.HighJumpGet();
					break;
				default:
					break;
			}
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
		}
	}


}

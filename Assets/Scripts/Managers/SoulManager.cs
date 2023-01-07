using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulManager : MonoBehaviour {
	float percentageFilled, totalSoul = 100f, soulFilled = 100f;
	int ghostsEnabled = 0;
	public Slider soulMeter;

	void Start() {
		GameEvents.current.OnGhostEnable += addEnabledGhost;
		GameEvents.current.OnGhostDisable += removeEnabledGhost;
		GameEvents.current.OnGetMoreSoul += getMoreSoul;
	}

	void Update() {
		percentageFilled = soulFilled / totalSoul;
		soulMeter.value = percentageFilled;
		soulFilled -= (float)ghostsEnabled * 11f * Time.deltaTime;
		if(ghostsEnabled == 0)
			soulFilled += 15f * Time.deltaTime;
		if (soulFilled <= 0f)
			GameEvents.current.GhostTimeout();
		soulFilled = Mathf.Clamp(soulFilled, 0f, totalSoul);
	}

	void addEnabledGhost() {
		ghostsEnabled++;
	}

	void removeEnabledGhost() {
		ghostsEnabled--;
	}

	void getMoreSoul() {
		totalSoul += 25f;
		soulFilled = totalSoul;
	}
}

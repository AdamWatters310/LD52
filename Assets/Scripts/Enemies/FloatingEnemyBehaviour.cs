using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemyBehaviour : MonoBehaviour {
	GameObject targetObject;
	float speed;
	void Start() {
		targetObject = Player.current.gameObject;
		speed = Random.value * 5f;
		speed = Mathf.Clamp(speed, 1.5f, 5f);
	}

	// Update is called once per frame
	void Update() {
		if (!LeanTween.isTweening(gameObject))
			LeanTween.move(gameObject, targetObject.transform.position, speed);
	}
}

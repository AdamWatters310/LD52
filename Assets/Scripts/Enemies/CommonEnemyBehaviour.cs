using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyBehaviour : MonoBehaviour {
	public float health = 1f;
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("PlayerProjectile")) {
			health -= 1f;
		}
	}

	private void Update() {
		if (health < 0f) {
			Destroy(gameObject);
		}
	}
}

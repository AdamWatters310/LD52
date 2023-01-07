using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableBlock : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("PlayerProjectile"))
			Destroy(gameObject);
	}
}

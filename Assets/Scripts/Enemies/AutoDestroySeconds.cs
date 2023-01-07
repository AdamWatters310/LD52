using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroySeconds : MonoBehaviour {
		public float destroyTime = 5f;
	void Start() {
		StartCoroutine(destroyProjectile());
	}

	private void Update() {
		
	}

	private IEnumerator destroyProjectile() {
		yield return new WaitForSeconds(destroyTime);
		Destroy(gameObject);
	}
}

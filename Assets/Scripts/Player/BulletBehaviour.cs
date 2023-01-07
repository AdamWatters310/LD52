using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

	bool destroy = false;
	Vector3 originalPos;

	public LayerMask ground, enemies, othercollidables;

	private void Awake() {
		originalPos = transform.position;
	}

	public void Shoot(float direction) {
		
		destroy = false;
		StartCoroutine(tick(7.5f, direction));				//7 units per second
	}

	public void Move(float distance, float direction) {
		RaycastHit2D groundRay = Physics2D.Raycast(transform.position, Vector2.right * direction, distance, ground);
		RaycastHit2D enemyRay = Physics2D.Raycast(transform.position, Vector2.right * direction, distance, enemies);
		RaycastHit2D otherRay = Physics2D.Raycast(transform.position, Vector2.right * direction, distance, ground);
		if (groundRay || enemyRay || otherRay) {
			Debug.Log("Collission registered with object " + distance + " units away");
			//distance = ray.distance;
			destroy = true;
		}
		transform.position += Vector3.right * direction * distance;

	}

	IEnumerator tick(float distance, float direction) {
		float elapsedTime = 0f;
		while (!destroy) {
			elapsedTime += Time.deltaTime;
			Move(distance * Time.deltaTime, direction);
			yield return null;
			if (elapsedTime >= 7f)
				break;
		}
		transform.position = originalPos;
		gameObject.SetActive(false);
	}
}
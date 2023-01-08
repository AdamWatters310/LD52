using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShooter : MonoBehaviour {

	public AudioSource soundEffect;

	float direction = 1f;
	public GameObject bullet;
	public BulletBehaviour[] bullets = new BulletBehaviour[15];
	int currentBullet;
	bool canShoot = false;

	void Awake() {
		currentBullet = 0;
		for(int i = 0; i < 15; i++) {
			bullets[i] = Instantiate(bullet, Vector3.zero, transform.rotation).GetComponent<BulletBehaviour>();
		}
	}

	private void Start() {
		GameEvents.current.OnShootEnable += ShooterEnabled;
		GameEvents.current.OnShootDisable += ShooterDisabled;
	}

	void Update() {
		if (Input.GetAxisRaw("Horizontal") > 0f)
			direction = 1f;
		else if (Input.GetAxisRaw("Horizontal") < 0f)
			direction = -1f;
		if (Input.GetKeyDown(KeyCode.X) && canShoot) {
			Debug.Log("x button pressed for bullet id " + currentBullet);
			bullets[currentBullet].gameObject.SetActive(true);
			bullets[currentBullet].transform.position = transform.position;
			bullets[currentBullet].Shoot(direction);
			currentBullet++;
			if (currentBullet >= 15) currentBullet = 0;
			soundEffect.Play();
		}
	}

	void ShooterEnabled() {
		canShoot = true;
	}

	void ShooterDisabled() {
		canShoot = false;
	}
}
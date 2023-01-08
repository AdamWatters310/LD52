using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCursor : MonoBehaviour {
	GameObject target;
	public Vector2 topLeftOfMap;
	public Vector2 coOrdinates;
	public new SpriteRenderer renderer;
	// Start is called before the first frame update
	void Start() {
		target = Player.current.gameObject;
	//	renderer = gameObject.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update() {
		coOrdinates.x = Mathf.FloorToInt((target.transform.position.x - topLeftOfMap.x) / 32f);
		coOrdinates.y = Mathf.FloorToInt((target.transform.position.y - topLeftOfMap.y) / 18f)+1;
		transform.localPosition = coOrdinates;
		
	}
}

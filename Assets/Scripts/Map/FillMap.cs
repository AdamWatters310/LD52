using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillMap : MonoBehaviour {

	public GameObject coverObject;

	void Start() {
		for(int i=0; i<20; i++) {
			for(int j=0; j> -20; j--) {
				GameObject currentObject = Instantiate(coverObject, transform);
				currentObject.transform.parent = transform;
				currentObject.transform.localPosition = new Vector3(i-1, j+1, 1f);
			}
		}
	}

}

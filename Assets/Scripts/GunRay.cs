using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRay : MonoBehaviour {
	[SerializeField] float distance = 4;
	[SerializeField] LayerMask explosivesLayer;

	void Update() {
		if (Input.GetButtonDown("Fire")) {
			if (Physics.Raycast(transform.position, Vector3.forward, distance, explosivesLayer)) {
				
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRay : MonoBehaviour {
	[SerializeField] float _distance = 4f;
	[SerializeField] float _force = 100f;
	[SerializeField] LayerMask explosivesLayer;
	[SerializeField] Transform explosivePivot;
	Rigidbody explosiveRigidbody = null;
	AudioSource gunSound;
	ExplosionController explosive;
	RaycastHit _beam;

	private void Awake() {
		gunSound = GetComponent<AudioSource>();
	}

	void Update() {
		if (Input.GetButtonDown("Fire1")) {
			if (explosiveRigidbody) {
				explosive = explosiveRigidbody.GetComponent<ExplosionController>();
				explosive.Activate(GetComponentInParent<PlayerStats>());
				explosiveRigidbody.AddForce(explosivePivot.forward * _force);
				explosiveRigidbody.useGravity = true;
				explosiveRigidbody = null;
				explosive = null;
				gunSound.Stop();
			} else {
				if (Physics.Raycast(transform.position, transform.forward,
						out _beam, _distance, explosivesLayer)) {

					explosiveRigidbody = _beam.rigidbody;
					explosiveRigidbody.useGravity = false;
					gunSound.Play();
				}
			}
		}
	}

	void FixedUpdate() {
		if (explosiveRigidbody) {
			explosiveRigidbody.position = explosivePivot.position;
			explosiveRigidbody.rotation = explosivePivot.rotation;
		}
	}

	private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(explosivePivot.position, 0.1f);
	}

}
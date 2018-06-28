using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRay : MonoBehaviour {
	[SerializeField] float _distance = 4f;
	[SerializeField] float _force = 100f;
	[SerializeField] LayerMask pickupsLayer;
	[SerializeField] Transform pickupPivot;
	Rigidbody pickupRigidbody = null;
	AudioSource gunSound;
	Pickup pickup;
	RaycastHit _beam;

	private void Awake() {
		gunSound = GetComponent<AudioSource>();
	}

	void Update() {
		if (Input.GetButtonDown("Fire1")) {
			if (pickupRigidbody) {
				pickup = pickupRigidbody.GetComponent<Pickup>();
				pickup.Activate(GetComponentInParent<CharacterStats>());
				pickupRigidbody.AddForce(pickupPivot.forward * _force);
				pickupRigidbody.useGravity = true;
				pickupRigidbody = null;
				pickup = null;
				gunSound.Stop();
			} else {
				if (Physics.Raycast(transform.position, transform.forward,
						out _beam, _distance, pickupsLayer)) {

					pickupRigidbody = _beam.rigidbody;
					pickupRigidbody.useGravity = false;
					gunSound.Play();
				}
			}
		}
	}

	void FixedUpdate() {
		if (pickupRigidbody) {
			pickupRigidbody.position = pickupPivot.position;
			pickupRigidbody.rotation = pickupPivot.rotation;
		}
	}

	private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(pickupPivot.position, 0.1f);
	}

}
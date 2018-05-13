using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {
	[SerializeField] float _jumpDuration = 1.5f;
	[SerializeField] float _jumpHeight = 1f;
	[SerializeField] float _gravityMultiplier = 10f;
	bool _isFalling = false;
	float _initialVelocity;
	float _jumpGravity;
	Rigidbody playerRigidbody;
	CapsuleCollider playerCollider;

	private void Awake() {
		playerRigidbody = GetComponent<Rigidbody>();
		playerCollider = GetComponent<CapsuleCollider>();
		_initialVelocity = 2f * _jumpHeight / _jumpDuration;
		_jumpGravity = -2f * _jumpHeight / (_jumpDuration * _jumpDuration);
	}

	private void Update() {
		if (Input.GetButtonDown("Jump") && IsGrounded()) {
			playerRigidbody.AddForce(Vector3.up * _initialVelocity, ForceMode.VelocityChange);
		}
		if (playerRigidbody.velocity.y < 0f && !_isFalling) {
			_jumpGravity *= _gravityMultiplier;
			_isFalling = true;
		}
		if (_isFalling && Physics.Raycast(transform.position, -Vector3.up, 1f)) {
			_jumpGravity /= _gravityMultiplier;
			_isFalling = false;
		}
	}

	void FixedUpdate() {
		playerRigidbody.AddForce(Vector3.up * _jumpGravity, ForceMode.Acceleration);
	}

	public bool IsGrounded() {
		Vector3 footBase = transform.position - (
			Vector3.up * playerCollider.height/2f);

		return (Physics.Raycast(footBase, Vector3.down, 0.01f));
	}
}
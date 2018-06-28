using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] float movementSpeed;
	[Range(1f, 100f)]
	[SerializeField] float breakDivisor;
	PlayerJump playerJump;
	Rigidbody playerRigidBody;
	Vector3 stopHorizontal;
	Vector3 stopVertical;

	void Start() {
		playerRigidBody = GetComponent<Rigidbody>();
		playerJump = GetComponent<PlayerJump>();
	}

	void FixedUpdate() {
		if (playerJump.IsGrounded()) {
			Movement();
		}
	}

	void Movement() {
		float moveVertical = Input.GetAxis("Vertical");
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
		if (Vector3.Magnitude(movement)> 1f) {
			movement = movement / Vector3.Magnitude(movement);
		}
		playerRigidBody.AddRelativeForce(movement * movementSpeed);
	}
}
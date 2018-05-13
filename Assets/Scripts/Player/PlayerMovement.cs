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
		// Vector3 localSpeed = transform.InverseTransformVector(
		// 	playerRigidBody.velocity);
		// if (moveHorizontal == 0f && localSpeed.x != 0f) {
		// 	stopHorizontal = transform.right * (-localSpeed.x / breakDivisor);
		// } else {
		// 	stopHorizontal = Vector3.zero;
		// }
		// if (moveVertical == 0f && localSpeed.z != 0f) {
		// 	stopVertical = transform.forward * (-localSpeed.z / breakDivisor);
		// } else {
		// 	stopVertical = Vector3.zero;
		// }
		// playerRigidBody.velocity += transform.TransformVector(stopVertical)+
		// 	transform.TransformVector(stopHorizontal);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] float movementSpeed;
	Rigidbody playerRigidBody;

	void Start() {
		playerRigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		float moveVertical = Input.GetAxis("Vertical");
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
		playerRigidBody.AddRelativeForce(movement * movementSpeed);
	}
}
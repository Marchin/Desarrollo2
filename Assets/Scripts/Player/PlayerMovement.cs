using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] float movementSpeed;
	// [Range(0f,90f)]
	[SerializeField] float rotationSpeed;
	Rigidbody playerRigidBody;

	void Start() {
		playerRigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		float moveVertical = Input.GetAxis("Vertical");
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector3(0f, 0.0f, moveVertical);
		// playerRigidBody.velocity = movement * movementSpeed * Time.deltaTime;
		playerRigidBody.AddRelativeForce(movement * movementSpeed);
		Vector3 rotation = new Vector3(0f, moveHorizontal, 0f)* rotationSpeed;
		// playerRigidBody.MoveRotation(Quaternion.Euler((transform.rotation.eulerAngles)* Time.deltaTime)* playerRigidBody.rotation);
		playerRigidBody.AddRelativeTorque(rotation);
	}
}
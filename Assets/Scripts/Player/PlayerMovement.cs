using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] float movementSpeed;
	[Range(0f,90f)]
	[SerializeField] float rotarionSpeed;
	Rigidbody playerRigidBody;

	void Start() {
		playerRigidBody = GetComponent<Rigidbody>();
	}

	void Update() {
		float moveVertical = Input.GetAxis("Vertical");
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector3(0f, 0.0f, moveVertical);
		playerRigidBody.velocity = movement * movementSpeed * Time.deltaTime;
		Vector3 rotation = new Vector3(0f,	moveHorizontal, 0f);
		playerRigidBody.rotation = Quaternion.Euler ( rotation * rotarionSpeed * Time.deltaTime);
	}
}
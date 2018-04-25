using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	[SerializeField] float rotationSpeed;
	[SerializeField] float min;
	[SerializeField] float max;
	Rigidbody playerRigidBody;

	void Start() {
		playerRigidBody = GetComponent<Rigidbody>();
	}

	void Update() {
		transform.rotation = Quaternion.Euler(new Vector3(
			Mathf.Clamp(transform.rotation.x, min, max),
			transform.rotation.y,
			transform.rotation.z)
		);
	}

	void FixedUpdate() {
		float cameraHorizontal = Input.GetAxis("Mouse X");
		float cameraVertical = Input.GetAxis("Mouse Y");
		Vector3 rotation = new Vector3(cameraHorizontal, cameraVertical, 0f)* rotationSpeed;
		playerRigidBody.AddRelativeTorque(rotation);
	}

}
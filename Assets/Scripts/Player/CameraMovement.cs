using UnityEngine;

public class CameraMovement : MonoBehaviour {
	[SerializeField] float _cameraX;
	[SerializeField] float _cameraY;
	[SerializeField] bool _invertedCam = true;
	[SerializeField] float _minimumX;
	[SerializeField] float _maximumX;
	Camera playerCamera;

	void Start() {
		playerCamera = GetComponentInChildren<Camera>();
	}

	void Update() {
		float cameraVertical = Input.GetAxis("Mouse Y")* (_invertedCam? - 1f : 1f);
		Vector3 rotationY = new Vector3(cameraVertical, 0f, 0f)* _cameraY;
		playerCamera.transform.Rotate(rotationY);
		playerCamera.transform.localRotation =
			ClampRotationAroundXAxis(playerCamera.transform.localRotation);

		float cameraHorizontal = Input.GetAxis("Mouse X");
		if (cameraHorizontal != 0f) {
			transform.Rotate(Vector3.up * cameraHorizontal * _cameraX);
		}
	}

	Quaternion ClampRotationAroundXAxis(Quaternion q) {
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

		angleX = Mathf.Clamp(angleX, _minimumX, _maximumX);

		q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}

}
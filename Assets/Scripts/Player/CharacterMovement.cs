using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    [SerializeField] float movementSpeed;
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;
    CharacterController cc;
    Vector3 movement = Vector3.zero;

    private void Awake() {
        cc = GetComponent<CharacterController>();

    }

    private void Update() {
        Movement();
    }

    void Movement() {
        if (cc.isGrounded) {
            movement = new Vector3(
                Input.GetAxis("Horizontal"),
                0f,
                Input.GetAxis("Vertical"));
            if (Vector3.Magnitude(movement)> 1f) {
                movement = movement / Vector3.Magnitude(movement);
            }
            movement = transform.TransformDirection(movement);
            movement *= movementSpeed;
            if (Input.GetButtonDown("Jump")) {
                movement.y = jumpForce;
            }
        }
        movement.y -= gravity * Time.deltaTime;
        cc.Move(movement * Time.deltaTime);
    }
    void Rotation() { }
}
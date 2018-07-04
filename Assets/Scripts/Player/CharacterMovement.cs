using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    [SerializeField] float m_movementSpeed;
    [SerializeField] float m_gravity;
    [SerializeField] float m_jumpForce;
    CharacterController cc;
    Vector3 movement = Vector3.zero;
    int maxJumps = 1;
    int availableJumps;

    private void Awake() {
        cc = GetComponent<CharacterController>();
        availableJumps = maxJumps;
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
            movement *= m_movementSpeed;
            availableJumps = maxJumps;
        }
        if (Input.GetButtonDown("Jump")&& availableJumps > 0) {
            Jump();
        }
        movement.y -= m_gravity * Time.deltaTime;
        cc.Move(movement * Time.deltaTime);
    }
    void Rotation() { }

    void Jump() {
        movement.y = m_jumpForce;
        availableJumps--;
    }

    public void SetJumpAmount(int amount, float duration) {
        maxJumps = amount;
        availableJumps = amount;
        Invoke("ResetMaxJumps", duration);
    }

    void ResetMaxJumps() {
        maxJumps = 1;
    }
}
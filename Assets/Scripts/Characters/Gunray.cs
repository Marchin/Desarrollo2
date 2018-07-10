using UnityEngine;

public class Gunray : MonoBehaviour {
	[SerializeField] float m_distance = 4f;
	[SerializeField] float m_force = 1000f;
	[SerializeField] LayerMask m_pickupsLayer;
	[SerializeField] Transform m_pickupPivot;
	Rigidbody m_pickupRigidbody = null;
	AudioSource m_gunSound;
	Pickup m_pickup;
	RaycastHit m_beam;

	private void Awake() {
		m_gunSound = GetComponent<AudioSource>();
	}

	void FixedUpdate() {
		if (m_pickupRigidbody) {
			m_pickupRigidbody.position = m_pickupPivot.position;
			m_pickupRigidbody.rotation = m_pickupPivot.rotation;
		}
	}

	public void Fire() {
		if (m_pickup) {
			m_pickup.Activate(GetComponentInParent<CharacterStats>());
			m_pickupRigidbody.AddForce(m_pickupPivot.forward * m_force);
			m_pickupRigidbody.useGravity = true;
			m_pickupRigidbody = null;
			m_pickup = null;
			m_gunSound.Stop();
		} else {
			if (Physics.Raycast(transform.position, transform.forward,
					out m_beam, m_distance, m_pickupsLayer)) {

				m_pickupRigidbody = m_beam.rigidbody;
				m_pickupRigidbody.useGravity = false;
				m_gunSound.Play();
				m_pickup = m_pickupRigidbody.GetComponent<Pickup>();
			}
		}
	}

	public Pickup GetPickup() {
		return m_pickup;
	}

	private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(m_pickupPivot.position, 0.1f);
	}

}
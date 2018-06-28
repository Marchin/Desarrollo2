using UnityEngine;

public class Pickup : MonoBehaviour {
	public delegate void PickupFunc(Collider[] receivers);
	PickupFunc m_pickupFunc;
	Properties m_properties;
	CharacterStats m_thrower;

	private void Update() {
		if (m_properties.state && m_properties.onContact) {
			Thrown();
		}
	}

	private void OnCollisionEnter() {
		m_properties.onContact = true;
	}

	void OnCollisionExit() {
		m_properties.onContact = false;
	}

	public void Thrown() {
		Collider[] affectedPlayers = Physics.OverlapSphere(transform.position,
			m_properties.radius, m_properties.affected.value);
		m_pickupFunc(affectedPlayers);

		m_properties.state = false;
		m_properties.particleFx.Play();
		m_properties.soundFx.pitch = Random.Range(0.8f, 1.2f);
		m_properties.soundFx.Play();
		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		Invoke("Disable", m_properties.particleFx.main.duration);
	}

	public void SetProperties(float radius, LayerMask affectedLayers, ParticleSystem explosionEffect,
		AudioSource explosionSound, PickupFunc pickupFunc) {

		m_properties.state = false;
		m_properties.onContact = false;
		m_properties.radius = radius;
		m_properties.affected = affectedLayers;
		m_properties.particleFx = explosionEffect;
		m_properties.soundFx = explosionSound;
		m_pickupFunc = pickupFunc;
	}

	public void Activate(CharacterStats thrower) {
		m_thrower = thrower;
		m_properties.state = true;
	}

	void Disable() {
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = false;
		m_properties.particleFx.Stop();
		gameObject.SetActive(false);
	}

	public CharacterStats GetThrower() {
		return m_thrower;
	}

	struct Properties {
		public bool state;
		public bool onContact;
		public float radius;
		public LayerMask affected;
		public ParticleSystem particleFx;
		public AudioSource soundFx;
	}

}
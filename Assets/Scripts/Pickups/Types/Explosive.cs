﻿using UnityEngine;

public class Explosive : MonoBehaviour {
	[SerializeField] int m_damage = 10;
	[SerializeField] float m_radius = 2f;
	[SerializeField] LayerMask m_affectedLayer;
	[SerializeField] ParticleSystem m_explosionEffect;
	[SerializeField] AudioSource m_explosionSound;
	Pickup pickup;

	private void Awake() {
		if (!(pickup = GetComponent<Pickup>())) {
			pickup = gameObject.AddComponent<Pickup>();
		}
		pickup.SetProperties(
			m_radius,
			m_affectedLayer,

			m_explosionEffect,
			m_explosionSound,
			Explode
		);
	}

	void Explode(Collider[] receivers) {
		CharacterStats player;
		CharacterStats thrower = pickup.GetThrower();
		foreach (Collider receiver in receivers) {
			player = receiver.GetComponent<CharacterStats>();
			if (player) {
				player.TakeDamage(m_damage);
			}
		}
		if (Vector3.Distance(transform.position, thrower.transform.position) < m_radius) {
			thrower.TakeDamage(m_damage);
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, m_radius);
	}
}
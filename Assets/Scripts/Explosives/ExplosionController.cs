using UnityEngine;
using UnityEngine.Events;

public class ExplosionController : MonoBehaviour {
	[SerializeField] float _radius = 2f;
	[SerializeField] int _damage = 10;
	[SerializeField] LayerMask playersLayer;
	[SerializeField] ParticleSystem explosionEffect;
	[SerializeField] AudioSource explosionSound;
    PlayerStats _thrower;
	bool _active;

	private void OnCollisionEnter() {
		if (_active) {
			Explode();
		}
	}

	void Explode() {
		RaycastHit[] damagedPlayers = Physics.SphereCastAll(transform.position,
			_radius, Vector3.up, 0.1f, playersLayer);
		PlayerStats player;
		foreach (RaycastHit damagedPlayer in damagedPlayers) {
			player = damagedPlayer.transform.gameObject.GetComponent<PlayerStats>();
			if (player) {
				player.TakeDamage(_damage);
				if (_thrower!=player){
					_thrower.AddScore(_damage);
				}
			} else {
				Debug.Log("Health not found");
			}
		}
		_active = false;
		explosionEffect.Play();
		explosionSound.pitch = Random.Range(0.8f, 1.2f);
		explosionSound.Play();
		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		Invoke("Disable", explosionEffect.main.duration);
	}

	public void Activate(PlayerStats thrower) {
		_thrower = thrower;
		_active = true;
	}

	void Disable() {
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = false;
		explosionEffect.Stop();
		gameObject.SetActive(false);
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _radius);
	}
}
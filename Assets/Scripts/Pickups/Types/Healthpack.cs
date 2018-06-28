using UnityEngine;

public class Healthpack : MonoBehaviour {
    [SerializeField] int m_heal = 10;
    [SerializeField] float m_radius;
    [SerializeField] ParticleSystem m_particleFx;
    [SerializeField] AudioSource m_soundFx;
    [SerializeField] LayerMask m_affected;
    Pickup pickup;

    private void Awake() {
        if (!(pickup = GetComponent<Pickup>())) {
            pickup = gameObject.AddComponent<Pickup>();
        }
        pickup.SetProperties(m_radius, m_affected, m_particleFx, m_soundFx, Heal);
    }

    void Heal(Collider[] receivers) {
        CharacterStats player;
        foreach (Collider receiver in receivers) {
            if (player = receiver.GetComponent<CharacterStats>()) {
                player.Heal(m_heal);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }
}
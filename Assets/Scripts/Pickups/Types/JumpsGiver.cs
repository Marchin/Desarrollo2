using UnityEngine;

public class JumpsGiver : MonoBehaviour {
    [SerializeField] int m_multiplier = 2;
    [SerializeField] float m_duration = 3f;
    [SerializeField] float m_radius;
    [SerializeField] ParticleSystem m_particleFx;
    [SerializeField] AudioSource m_soundFx;
    [SerializeField] LayerMask m_affected;
    Pickup pickup;

    private void Awake() {
        if (!(pickup = GetComponent<Pickup>())) {
            pickup = gameObject.AddComponent<Pickup>();
        }
        pickup.SetProperties(m_radius, m_affected, m_particleFx, m_soundFx, GiveJumps);
    }

    void GiveJumps(Collider[] receivers) {
        CharacterMovement player;
        foreach (Collider receiver in receivers) {
            if (player = receiver.GetComponent<CharacterMovement>()) {
                player.SetJumpAmount(m_multiplier, m_duration);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }
}
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
    LineRenderer m_lineRenderer;
    bool aimingPickup = false;

    private void Awake() {
        m_gunSound = GetComponent<AudioSource>();
        m_lineRenderer = GetComponentInChildren<LineRenderer>();
        m_lineRenderer.SetPosition(1, new Vector3(0f, 0f, m_distance));
    }

    void FixedUpdate() {
        if (m_pickupRigidbody) {
            m_pickupRigidbody.position = m_pickupPivot.position;
            m_pickupRigidbody.rotation = m_pickupPivot.rotation;
        }
    }

    private void LateUpdate() {
        if (Physics.Raycast(transform.position, transform.forward,
            out m_beam, m_distance, m_pickupsLayer)) {

            if (!aimingPickup) {
                AimColor(Color.green);
                aimingPickup = true;
            }
        } else {

            if (aimingPickup) {
                AimColor(Color.red);
                aimingPickup = false;
            }
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

    void AimColor(Color newColor) {
        Gradient gradient = new Gradient();
        ;
        GradientColorKey[] colorKey = new GradientColorKey[1];
        colorKey[0].color = newColor;
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha = 1f;
        gradient.SetKeys(colorKey, alphaKey);
        m_lineRenderer.colorGradient = gradient;
    }

    public Pickup GetPickup() {
        return m_pickup;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(m_pickupPivot.position, 0.1f);
    }

}
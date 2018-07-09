using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
    [SerializeField] LayerMask m_targetLayer;
    [SerializeField] float m_searchRadius;
    [SerializeField] float m_searchInterval;
    [SerializeField] LayerMask m_team;
    [SerializeField] LayerMask m_oppositeTeam;
    NavMeshAgent m_agent;
    GunRay m_gunRay;
    CharacterStats characterStats;
    Collider m_pickupFound;
    Pickup m_currentPickup;
    float m_searchingTime;

    private void Awake() {
        m_searchingTime = m_searchInterval;
        m_gunRay = GetComponent<GunRay>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (!m_pickupFound) {
            Search();
        } else if (!m_currentPickup) {
            transform.LookAt(m_pickupFound.transform.position);
            m_gunRay.Fire();
            m_currentPickup = m_gunRay.GetPickup();
        }
    }
    //getpickup layers
    void Search() {
        m_agent.SetDestination(transform.forward);
        m_searchingTime += Time.deltaTime;
        if (m_searchingTime > m_searchInterval) {
            m_pickupFound = Physics.OverlapSphere(transform.position, m_searchRadius, m_targetLayer)[0];
            m_searchingTime = 0f;
        }
    }

    void LookForTarget() {

    }
}
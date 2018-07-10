using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
    [SerializeField] float m_searchRadius;
    [SerializeField] float m_searchInterval;
    [SerializeField] float m_turnRate;
    [SerializeField] bool m_isEnemy = false;
    [SerializeField] Transform m_aim;
    [SerializeField] Transform[] m_patrolPoints;
    LayerMask m_targetLayer;
    NavMeshAgent m_agent;
    Gunray m_gunRay;
    Collider m_fireTarget;
    Collider m_pickupFound;
    Pickup m_currentPickup;
    float m_searchingTime;
    int fails;

    private void Awake() {
        m_targetLayer = LayerMask.GetMask("Pickups");
        m_gunRay = GetComponentInChildren<Gunray>();
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.destination = transform.position;
        m_searchingTime = m_searchInterval;
    }

    private void Update() {
        if (!m_pickupFound) {
            SearchTarget(ref m_pickupFound);
        } else if (!m_currentPickup) {
            Pick();
        } else if (!m_fireTarget) {
            SearchTarget(ref m_fireTarget);
        } else {
            FireTarget();
        }
    }

    void SearchTarget(ref Collider target) {
        if (!m_agent.hasPath) {
            Patrol();
        }
        m_searchingTime += Time.deltaTime;
        if (m_searchingTime > m_searchInterval) {
            Collider[] colliders = Physics.OverlapSphere(transform.position,
                m_searchRadius, m_targetLayer);
            if (colliders.Length >= 1) {
                target = colliders[0];
            } else {
                Patrol();
            }
            m_searchingTime = 0f;
        }
    }

    void Patrol() {
        int point = Random.Range(0, m_patrolPoints.Length - 1);
        m_agent.SetDestination(m_patrolPoints[point].position);
    }

    void Pick() {
        Look(m_pickupFound.transform.position);
        m_agent.SetDestination(m_pickupFound.transform.position);
        //if (!m_agent.hasPath) {
        m_gunRay.Fire();
        m_currentPickup = m_gunRay.GetPickup();
        if (m_currentPickup != null) {
            if (m_isEnemy) {
                m_currentPickup.SetToEnemy();
            }
            m_targetLayer = m_currentPickup.GetTarget();
            Success();
        } else {
            Fail();
        }
        //}
    }

    void FireTarget() {
        Look(m_fireTarget.transform.position);
        if (!m_agent.hasPath) {
            m_gunRay.Fire();
            Restart();
        }
    }

    void Look(Vector3 targetPosition) {
        m_aim.LookAt(targetPosition);
        transform.rotation = new Quaternion(
            transform.rotation.x,
            m_aim.rotation.y,
            transform.rotation.z,
            transform.rotation.w
        );
    }

    void Restart() {
        m_targetLayer = LayerMask.GetMask("Pickups");
        m_pickupFound = null;
        m_currentPickup = null;
        m_fireTarget = null;
        fails = 0;
    }

    void Fail() {
        fails++;
        if (fails == 5) {
            Restart();
        }
    }

    void Success() {
        fails = 0;
    }
}
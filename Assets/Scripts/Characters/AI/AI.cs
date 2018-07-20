using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
    [SerializeField] PatrolPointsController m_ppController;
    [SerializeField] float m_searchRadius;
    [SerializeField] float m_searchInterval;
    [SerializeField] float m_throwingDistance = 10f;
    [SerializeField] bool m_isEnemy = false;
    [SerializeField] Transform m_aim;
    LayerMask m_targetLayer;
    NavMeshAgent m_agent;
    Gunray m_gunRay;
    Collider m_fireTarget;
    Collider m_pickupFound;
    Pickup m_currentPickup;
    float m_searchingTime;
    int m_currentPoint = -1;
    int m_fails;

    private void Awake() {
        m_targetLayer = LayerMask.GetMask("Pickups");
        m_gunRay = GetComponentInChildren<Gunray>();
        m_agent = GetComponent<NavMeshAgent>();
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
            Collider[] potentialTargets = Physics.OverlapSphere(transform.position,
                m_searchRadius, m_targetLayer);
            target = null;
            if (potentialTargets.Length >= 1) {
                foreach (Collider potentialTarget in potentialTargets) {
                    if (m_targetLayer == LayerMask.GetMask("Pickups")) {
                        if (!potentialTarget.GetComponent<Pickup>().IsAvailable()) {
                            continue;
                        }
                    }
                    if (!target || (Vector3.Distance(transform.position, potentialTarget.transform.position) <
                            Vector3.Distance(transform.position, target.transform.position))) {

                        target = potentialTarget;
                    }
                }
                if (target) {
                    m_agent.SetDestination(target.transform.position);
                } else {
                    Patrol();
                }
            } else {
                Patrol();
            }
            m_searchingTime = 0f;
        }
    }

    void Patrol() {
        Vector3 newPosition = transform.position;
        if (m_ppController.RequestPatrolPoint(ref m_currentPoint,
                ref newPosition, m_isEnemy)) {

            m_agent.SetDestination(newPosition);
        }
    }

    void Pick() {
        Look(m_pickupFound.transform.position);
        m_gunRay.Fire();
        m_currentPickup = m_gunRay.GetPickup();
        if (m_currentPickup != null) {
            if (m_isEnemy) {
                m_currentPickup.SetToEnemy();
            }
            m_targetLayer = m_currentPickup.GetTargetLayer();
            Vector3 lookStraight = m_aim.rotation.eulerAngles;
            lookStraight.x = -15;
            m_aim.eulerAngles = lookStraight;
            Success();
        } else {
            Fail();
        }
    }

    void FireTarget() {
        Look(m_fireTarget.transform.position);
        if (Vector3.Distance(transform.position, m_fireTarget.transform.position) <
            m_throwingDistance) {

            m_gunRay.Fire();
            Restart();
        }
    }

    void Look(Vector3 targetPosition) {
        m_aim.LookAt(targetPosition);
        Quaternion rot = Quaternion.Euler(
            transform.eulerAngles.x,
            m_aim.eulerAngles.y,
            transform.eulerAngles.z
        );
        Quaternion.RotateTowards(transform.rotation, rot, m_agent.angularSpeed);
    }

    void Restart() {
        m_targetLayer = LayerMask.GetMask("Pickups");
        m_pickupFound = null;
        m_currentPickup = null;
        m_fireTarget = null;
        m_searchingTime = 0;
        m_fails = 0;
    }

    void Fail() {
        m_fails++;
        if (m_fails == 25) {
            Restart();
        }
    }

    void Success() {
        m_fails = 0;
    }

    public bool IsEnemy() {
        return m_isEnemy;
    }
}
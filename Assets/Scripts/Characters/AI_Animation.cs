using UnityEngine;
using UnityEngine.AI;

public class AI_Animation : MonoBehaviour {
    [SerializeField] Transform m_aim;
    NavMeshAgent m_agent;
    Vector3 m_prevPos;
    Animator m_animator;
    Vector3 speed;
    float timer;
    float rotation;

    private void Awake() {
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        m_prevPos = transform.position;
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= 0.1f) {
            Vector3 speed = transform.position - m_prevPos;
            speed = transform.InverseTransformDirection(speed);
            m_animator.SetFloat("Speed", (speed.z / m_agent.speed) * 10);
            m_animator.SetFloat("SideSpeed", (speed.x / m_agent.speed) * 10);
            timer = 0f;
            m_prevPos = transform.position;
            rotation = m_aim.rotation.eulerAngles.x;
            if (rotation > 180) {
                rotation = 360 - rotation;
            }
            m_animator.SetFloat("Aim", rotation);
        }
    }
}
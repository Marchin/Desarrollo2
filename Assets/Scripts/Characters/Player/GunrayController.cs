using UnityEngine;

public class GunrayController : MonoBehaviour {
    Gunray m_gunray;

    private void Awake() {
        m_gunray = GetComponent<Gunray>();
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            m_gunray.Fire();
        }
    }
}
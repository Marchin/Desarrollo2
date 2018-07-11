using UnityEngine;

public class ReflectionRefresher : MonoBehaviour {
    [SerializeField] float refreshPerSec = 24f;
    ReflectionProbe reflection;
    float timer = 0f;
    float refreshRate;

    private void Awake() {
        reflection = GetComponent<ReflectionProbe>();
        refreshRate = 1 / refreshPerSec;
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer > refreshRate) {
            reflection.RenderProbe();
            timer = 0f;
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager _instantiate;

    private void Awake() {
        if (_instantiate) {
            Destroy(gameObject);
        } else {
            _instantiate = this;
        }
    }

    public void Victory() {
        SceneManager.LoadScene("Victory");
        Cursor.visible = true;
    }

    public void Defeat() {
        SceneManager.LoadScene("Defeat");
        Cursor.visible = true;
    }
}
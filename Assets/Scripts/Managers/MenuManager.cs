using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public static MenuManager _instantiate;

    private void Awake() {
        if (_instantiate) {
            Destroy(gameObject);
        } else {
            _instantiate = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartGame() {
        SceneManager.LoadScene("Level");
    }

    public void ExitGame() {
        Application.Quit();
    }

}
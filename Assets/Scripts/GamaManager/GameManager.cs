using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    private void Awake() {
        if(Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        Time.timeScale = 1;
    }

    private void Update() {
        HUD.Instance.MetersHandler();
        HUD.Instance.HpHandler();
    }

    public void RestartGame() {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}

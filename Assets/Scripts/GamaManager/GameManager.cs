using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public GameObject PauseScreen;
    public AudioSource GameplayAudio;
    public float meters = 0;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        Time.timeScale = 1;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0;
            PauseScreen.SetActive(true);
        }
        MetersHandler();
        HUD.Instance.HpHandler();
    }

    public void MetersHandler(int RateMeters = 10)
    {
        meters += Time.deltaTime * RateMeters;
        HUD.Instance.ShowMeters();
    }

    public void PauseGameMobile()
    {
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
    }

    // Buttons Pause Screen
    public void RestartGame() {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void ResumeGame() {
        PauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void MuteAudio() {
        GameplayAudio.volume = 0;
    }
}

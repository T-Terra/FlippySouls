using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance {get; set;}

    public float meters;
    public Slider hp;
    public Slider xp;
    public TMP_Text souls;
    public TMP_Text metersDistance;
    public GameObject PowerUpScreen;
    public GameObject ResultsScreen;
    public TMP_Text metersDistanceResults;
    public TMP_Text metersHighResults;
    private GameObject player;

    private void Awake() {
        if(Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        metersHighResults.text = PlayerPrefs.GetFloat("high").ToString("F0");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ExpHandler( float points = 10 ) {
        if(xp.value == xp.maxValue) {
            xp.value = 0;
            xp.maxValue += 200;
        }
        xp.value = points;
    }

    public void HpHandler( float time = 5) {
        /*if(Time.time > 60) {
            time = 10;
        }*/
        hp.value -= Time.deltaTime * time;
        player.GetComponent<PlayerMovement>().stats.hp -= Time.deltaTime * time;
        
        if(hp.value == hp.minValue) {
            hp.value = hp.maxValue;
        }
        
        if(player.GetComponent<PlayerMovement>().stats.hp <= hp.minValue) {
            Time.timeScale = 0;
            ResultsScreen.SetActive(true);
            metersDistanceResults.text = (meters).ToString("F0");

            // save high score
            if (!PlayerPrefs.HasKey("high")) {
                PlayerPrefs.SetFloat("high", meters);
            } else if (PlayerPrefs.GetFloat("high") < meters) {
                PlayerPrefs.SetFloat("high", meters);
            }
        }
    }

    public void HpAdd( float newHp ) {
        hp.value = newHp;
    }

    public void HpRemove( float newHp ) {
        hp.value = newHp;
    }

    public void MetersHandler( int RateMeters = 10 ) {
        meters += Time.deltaTime * RateMeters;
        metersDistance.text = (meters).ToString("F0") + "M";
    }

    public void SoulsHandler( float soulsColleted = 0) {
        souls.text = soulsColleted.ToString();
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance {get; private set;}

    public Slider xp;
    public TMP_Text souls;
    public TMP_Text metersDistance;
    public GameObject PowerUpScreem;

    private void Awake() {
        if(Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        ExpHandler(20);
        ExpHandler(25);
    }

    public void ExpHandler( float points = 10 ) {
        if(xp.value == xp.maxValue) {
            PowerUpScreem.SetActive(true);
            xp.value = 0;
        }
        xp.value += points;
    }

    public void MetersHandler( int RateMeters = 10 ) {
        float meters = Time.time;
        metersDistance.text = (meters * RateMeters).ToString("F0") + "M";
    }

    public void SoulsHandler( int soulsColleted = 0) {
        souls.text = soulsColleted.ToString();
    }
}

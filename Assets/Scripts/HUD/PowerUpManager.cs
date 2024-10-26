using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    public PowerUpCards[] PowerUpAttributes;
    public GameObject[] CardsPowerUp;

    private int count = 0;

    private void Awake() {
        if(Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating(nameof(SpreadCards), 0, 1f * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpreadCards() {
        int Num = Random.Range(0, 15);
        CardsPowerUp[count].GetComponent<Image>().sprite = PowerUpAttributes[0].spriteRender;
        if(count == 2) {
            CancelInvoke(nameof(SpreadCards));
        }
        count++;
    }
}

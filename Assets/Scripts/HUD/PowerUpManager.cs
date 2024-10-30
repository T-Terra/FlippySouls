using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    public List<PowerUpCards> PowerUpAttributes;

    public List<PowerUpCards> PowerUpAttributesV2;
    public GameObject[] CardsPowerUp;

    public GameObject PowerUpScreem;
    private PlayerMovement Player;

    private int count = 0;
    private int SortNumPrevious = 0;
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
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Player.OnActivatedPowerUp += CoroutineCards;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CoroutineCards() {
        StartCoroutine(SpreadCards());
    }

    public IEnumerator SpreadCards() {
        int i = 0;
        while (true)
        {
            int numRandom = Random.Range(0, 5);

            var newPowerUp = PowerUpAttributesV2[numRandom];
            
            // Verifica se o ID do novo PowerUp já está presente na lista
            bool idExists = PowerUpAttributes.Any(powerUp => powerUp.ID == newPowerUp.ID);
            print(idExists);
        
            if (!idExists) {
                print(PowerUpAttributes.Count);
                PowerUpAttributes.Add(newPowerUp);

                CardsPowerUp[i].GetComponent<Image>().sprite = PowerUpAttributes[i].spriteRender;
                print(PowerUpAttributes[i]);
                i++;
            }

            if(i == 3) {
                break;
            }
            yield return null;
        }
    }

    public void CleanList( PowerUpCards Item ) {
        for (int i = 0; i < PowerUpAttributesV2.Count; i++)
        {
            if(PowerUpAttributesV2[i].ID == Item.ID) {
                PowerUpAttributesV2.RemoveAt(i);
            }
        }
    }

    public void SelectPower( int Item ) {
        CleanList(PowerUpAttributes[Item]);
        PowerUpAttributes.Clear();
        PowerUpScreem.SetActive(false);
        Time.timeScale = 1;
    }
}

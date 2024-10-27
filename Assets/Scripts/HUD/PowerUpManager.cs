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
        InvokeRepeating(nameof(SpreadCards), 0, 1f * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpreadCards() {
        int numRandom = Random.Range(0, 5);

        if (numRandom != SortNumPrevious) {
            var newPowerUp = PowerUpAttributesV2[numRandom];
            
            // Verifica se o ID do novo PowerUp já está presente na lista
            bool idExists = PowerUpAttributes.Any(powerUp => powerUp.ID == newPowerUp.ID);
            
            if (!idExists) {
                SortNumPrevious = numRandom;
                PowerUpAttributes.Add(newPowerUp);

                CardsPowerUp[count].GetComponent<Image>().sprite = PowerUpAttributes[count].spriteRender;
                count++;
            
                if (count == 3) {
                    CancelInvoke(nameof(SpreadCards));
                }
            }
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
        PowerUpAttributes.RemoveAt(Item);
    }
}

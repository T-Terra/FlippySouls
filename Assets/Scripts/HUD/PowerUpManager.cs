using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    public List<PowerUpCards> PowerUpAttributes;
    public List<PowerUpCards> PowerUpBible;
    public List<PowerUpCards> PowerUpHadounken;
    public List<PowerUpCards> PowerUpIma;
    public List<PowerUpCards> PowerUpFoice;
    public List<PowerUpCards> PowerUpShield;
    public GameObject[] CardsPowerUp;

    public GameObject PowerUpScreen;
    public GameObject[] bibleObj;
    private PlayerMovement Player;
    private PowerUpCards newPowerUp;
    public SkillsManager SkillManager;
    public GameObject AtivateSkill;
    public ShieldManager ShieldManager;
    public GameObject ActiveShield;
    public int SelectedSkill;
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

            if(numRandom == 0 && PowerUpBible.Count != 0) {
                newPowerUp = PowerUpBible[0];
            } else if (numRandom == 1 && PowerUpFoice.Count != 0) {
                newPowerUp = PowerUpFoice[0];
            } else if (numRandom == 2 && PowerUpHadounken.Count != 0) {
                newPowerUp = PowerUpHadounken[0];
            } else if (numRandom == 3 && PowerUpIma.Count != 0) {
                newPowerUp = PowerUpIma[0];
            } else if (numRandom == 4 && PowerUpShield.Count != 0) {
                newPowerUp = PowerUpShield[0];
            }
            
            // Verifica se o ID do novo PowerUp já está presente na lista
            bool idExists = PowerUpAttributes.Any(powerUp => powerUp.ID == newPowerUp.ID);
        
            if (!idExists && newPowerUp != null) {
                PowerUpAttributes.Add(newPowerUp);

                CardsPowerUp[i].GetComponent<Image>().sprite = PowerUpAttributes[i].spriteRender;
                i++;
            }

            if(PowerUpAttributes.Count == 3) {
                PowerUpScreen.SetActive(true);
                Time.timeScale = 0;
            }

            if(i == 3) {
                break;
            }
            yield return null;
        }
    }

    public IEnumerator CleanList( PowerUpCards Item ) {

        if(SelectedSkill == 0) {
            if(PowerUpBible.Count == 1) {
                PowerUpBible.Clear();
            } else {
                PowerUpBible.RemoveAt(0);
            }
        } else if (SelectedSkill == 1) {
            if(PowerUpFoice.Count == 1) {
                PowerUpFoice.Clear();
            } else {
                PowerUpFoice.RemoveAt(0);
            }
        } else if (SelectedSkill == 2) {
            if(PowerUpHadounken.Count == 1) {
                PowerUpHadounken.Clear();
            } else {
                PowerUpHadounken.RemoveAt(0);
            }
        } else if (SelectedSkill == 3) {
            if(PowerUpIma.Count == 1) {
                PowerUpIma.Clear();
            } else {
                PowerUpIma.RemoveAt(0);
            }
        } else if (SelectedSkill == 4) {
            if(PowerUpShield.Count == 1) {
                PowerUpShield.Clear();
            } else {
                PowerUpShield.RemoveAt(0);
            }
        }
        
        yield return null;
    }

    public void SelectPower( int Item ) {
        if(PowerUpAttributes.Count != 0) {
            SelectSkill(PowerUpAttributes[Item]);
            StartCoroutine(CleanList(PowerUpAttributes[Item]));
            PowerUpAttributes.Clear();
            PowerUpScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void SelectSkill( PowerUpCards Item ) {
        if(Item.ID < 6) {
            switch (Item.ID)
            {
                case 1:
                    BiblePower(Item.bibleTotal);
                    SelectedSkill = 0;
                    break;
                case 2:
                    FoicePower(Item.darkPowerArea);
                    SelectedSkill = 1;
                    break;
                case 3:
                    HadounkenPower(Item.hadoukenTime);
                    SelectedSkill = 2;
                    break;
                case 4:
                    ImaPower();
                    SelectedSkill = 3;
                    break;
                case 5:
                    ShieldPower(Item.timeShield, Item.ShieldInterval);
                    SelectedSkill = 4;
                    break;
                default:
                    break;
            }           
        } else if (Item.ID >= 6 && Item.ID <= 10) {
            switch (Item.ID)
            {
                case 6:
                    BiblePower(Item.bibleTotal);
                    SelectedSkill = 0;
                    break;
                case 7:
                    FoicePower(Item.darkPowerArea);
                    SelectedSkill = 1;
                    break;
                case 8:
                    HadounkenPower(Item.hadoukenTime);
                    SelectedSkill = 2;
                    break;
                case 9:
                    ImaPower();
                    SelectedSkill = 3;
                    break;
                case 10:
                    ShieldPower(Item.timeShield, Item.ShieldInterval);
                    SelectedSkill = 4;
                    break;
                default:
                    break;
            } 
        } else if (Item.ID > 10 && Item.ID < 16) {
            switch (Item.ID)
            {
                case 11:
                    BiblePower(Item.bibleTotal);
                    SelectedSkill = 0;
                    break;
                case 12:
                    FoicePower(Item.darkPowerArea);
                    SelectedSkill = 1;
                    break;
                case 13:
                    HadounkenPower(Item.hadoukenTime);
                    SelectedSkill = 2;
                    break;
                case 14:
                    ImaPower();
                    SelectedSkill = 3;
                    break;
                case 15:
                    ShieldPower(Item.timeShield, Item.ShieldInterval);
                    SelectedSkill = 4;
                    break;
                default:
                    break;
            } 
        }
    }

    private void BiblePower( int total ) {
        if(total == 1) {
            bibleObj[0].SetActive(true);
            Player.stats.levelBible += 1;
        } else if (total == 2) {
            bibleObj[1].SetActive(true);
            Player.stats.levelBible += 1;
        } else if (total == 3) {
            bibleObj[2].SetActive(true);
            Player.stats.levelBible += 1;
        }
    }

    private void HadounkenPower( float time ) {
        AtivateSkill.SetActive(true);
        SkillManager.newInterval = time;
        if(Player.stats.levelHadounken <= 3) {
            Player.stats.levelHadounken += 1;
        }   
    }

    private void ImaPower() {
        Player.stats.levelIma += 1;
    }

    private void FoicePower( float area ) {
        Player.radius = area;
        if(Player.stats.levelFoice <= 3) {
            Player.stats.levelFoice += 1;
        }
    }

    private void ShieldPower( float timeShield, float shieldInterval ) {
        if(Player.stats.levelShield <= 3) {
            Player.stats.levelShield += 1;
        }

        if(Player.stats.levelShield == 1) {
            ActiveShield.SetActive(true);
        }

        ShieldManager.newInterval = shieldInterval;
        ShieldManager.newtimeActive = timeShield;
    }
}

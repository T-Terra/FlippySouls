using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public float timeInterval = 8f;
    public float newInterval = 8f;
    public GameObject Spell;
    private Transform PlayerTransform;
    private bool CanAtk = true;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        SpawnSpell();
    }

    private void SpawnSpell() {
        if(CanAtk) {
            Instantiate(Spell, PlayerTransform.position, Quaternion.identity);
            CanAtk = false;
        }
        IntervalAtk();
    }

    private void IntervalAtk() {
        if(CanAtk == false && timeInterval <= 0) {
            CanAtk = true;
            timeInterval = newInterval;
        } else {
            timeInterval -= Time.deltaTime;
        }
    }
}

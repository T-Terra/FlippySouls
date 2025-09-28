using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public Transform PointAttack;
    public float radius = 1f;
    public float timeInterval = 8f;
    public float newInterval = 8f;
    public float timeActive = 1f;
    public float newtimeActive = 1f;
    public LayerMask LayerAtk;
    private PlayerMovement PlayerStats;
    private bool CanAtk = true;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update() {
        SpawnSpell();
    }

    private void SpawnSpell() {
        if(CanAtk) {
            IntervalForActive();
            Attack();
        }
        IntervalAtk();
    }

    private void IntervalAtk() {
        if(CanAtk == false && timeInterval <= 0) {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            CanAtk = true;
            timeInterval = newInterval;
        } else {
            timeInterval -= Time.deltaTime;
        }
    }

    private void IntervalForActive() {
        if(timeActive <= 0) {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            CanAtk = false;
            timeActive = newtimeActive;
        } else {
            timeActive -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos() {
        if(this.PointAttack != null) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.PointAttack.position, this.radius);
        }
    }

    private void Attack() {
        Collider2D[] colliderEnemy = Physics2D.OverlapCircleAll(this.PointAttack.position, this.radius, this.LayerAtk);
        if(colliderEnemy != null) {
            foreach (Collider2D Enemy in colliderEnemy)
            {
                Destroy(Enemy.gameObject);

                if (PlayerStats.stats.hp < PlayerStats.stats.maxHP)
                {
                    PlayerStats.stats.hp += 0.1f;
                    PlayerStats.stats.xp += 5;
                    PlayerStats.stats.ExpHandler(PlayerStats.stats.xp);
                    HUD.Instance.HpAdd(PlayerStats.stats.hp);

                }
                else if (PlayerStats.stats.hp > PlayerStats.stats.maxHP)
                {
                    PlayerStats.stats.hp = PlayerStats.stats.maxHP;
                }
            }
        }
    }
}

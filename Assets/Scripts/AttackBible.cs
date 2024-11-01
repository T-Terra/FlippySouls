using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBible : MonoBehaviour
{
    public int damageBible;
    private PlayerMovement StatsPlayer;
    public LayerMask LayerAttack;
    public Transform PointAttack;
    public float radiusB = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StatsPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void OnDrawGizmos() {
        if(this.PointAttack != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.PointAttack.position, this.radiusB);
        }
    }

    private void Attack() {
        Collider2D[] colliderEnemy = Physics2D.OverlapCircleAll(this.PointAttack.position, this.radiusB, this.LayerAttack);
        if(colliderEnemy != null) {

            foreach (Collider2D Enemy in colliderEnemy)
            {
                Enemies enemyStats = Enemy.gameObject.GetComponent<Enemies>();
                if(enemyStats != null) {
                    UtilsFunc.TakeDamage(Enemy.gameObject, StatsPlayer.stats.baseAttack);

                    if (enemyStats.stats.hp <= 0)
                    {
                        if (StatsPlayer.stats.hp < StatsPlayer.stats.maxHP)
                        {
                            StatsPlayer.stats.hp += 1f;
                            StatsPlayer.stats.xp += enemyStats.stats.xp;
                            HUD.Instance.ExpHandler(StatsPlayer.stats.xp);
                            HUD.Instance.HpAdd(StatsPlayer.stats.hp);

                        }
                        else if (StatsPlayer.stats.hp > StatsPlayer.stats.maxHP)
                        {
                            StatsPlayer.stats.hp = StatsPlayer.stats.maxHP;
                        }
                    }
                }
            }
        }
    }
}

using UnityEngine;

public class SpellPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public int damage;
    private PlayerMovement StatsPlayer;
    public LayerMask LayerAttack;
    public Transform PointAttack;
    public float radiusB = 1f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        StatsPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Destroy(gameObject, 10);
    }
    void Update()
    {
        rb.velocity = new Vector2(speed, 0);
        Attack();
    }

     private void OnDrawGizmos() {
        if(this.PointAttack != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.PointAttack.position, this.radiusB);
        }
    }

    private void Attack() {
        Collider2D colliderEnemy = Physics2D.OverlapCircle(this.PointAttack.position, this.radiusB, this.LayerAttack);
        if(colliderEnemy != null) {
            Enemies enemyStats = colliderEnemy.gameObject.GetComponent<Enemies>();
            if(enemyStats != null) {
                UtilsFunc.TakeDamage(colliderEnemy.gameObject, StatsPlayer.stats.baseAttack);

                if (enemyStats.stats.hp <= 0)
                {
                    if (StatsPlayer.stats.hp < StatsPlayer.stats.maxHP)
                    {
                        StatsPlayer.stats.hp += 5f;
                        StatsPlayer.stats.xp += enemyStats.stats.xp;
                        StatsPlayer.stats.ExpHandler(StatsPlayer.stats.xp);
                        HUD.Instance.HpAdd(StatsPlayer.stats.hp);
                    }
                    else if (StatsPlayer.stats.hp > StatsPlayer.stats.maxHP)
                    {
                        StatsPlayer.stats.hp = StatsPlayer.stats.maxHP;
                        
                    }
                    Destroy(gameObject);
                }
            }   
        }
    }
}

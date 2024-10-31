using UnityEngine;

public class BibleRotation : MonoBehaviour
{
    private float angle = 0f;
    public float radius = 5f; // Raio do movimento circular
    public float angularSpeed; // Velocidade angular (em radianos por segundo)
    public int damageBible;
    private PlayerMovement StatsPlayer;

    private void Start() {
        StatsPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotationBible();
    }

    private void RotationBible() {
        // Calcula o ângulo baseado no tempo e na velocidade angular
        float direction = -1f;
        angle = direction * angularSpeed * radius;

        // Cria um novo vetor para representar a rotação no eixo Z (ou qualquer outro eixo)
        Vector3 rotationAxis = new Vector3(0, 0, 1 * direction); // Eixo Z como exemplo

        // Aplica a rotação ao redor do eixo definido
        transform.Rotate(rotationAxis, angle * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Medusa" || collision.gameObject.tag == "Pumpking" || collision.gameObject.tag == "Stalker" || collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Default") {
            Stats enemyStats = collision.gameObject.GetComponent<Enemies>().stats;
            UtilsFunc.TakeDamage(collision.gameObject, damageBible);

            if (enemyStats.hp <= 0)
            {
                if (StatsPlayer.stats.hp < StatsPlayer.stats.maxHP) {
                    StatsPlayer.stats.hp += 10;
                    StatsPlayer.stats.xp += enemyStats.xp;
                    HUD.Instance.ExpHandler(StatsPlayer.stats.xp);
                    HUD.Instance.HpAdd(StatsPlayer.stats.hp);
                } else if(StatsPlayer.stats.hp > StatsPlayer.stats.maxHP) {
                    StatsPlayer.stats.hp = StatsPlayer.stats.maxHP;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Medusa" || collision.gameObject.tag == "Pumpking" || collision.gameObject.tag == "Stalker" || collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Default") {
            Stats enemyStats = collision.gameObject.GetComponent<Enemies>().stats;
            UtilsFunc.TakeDamage(collision.gameObject, damageBible);

            if (enemyStats.hp <= 0)
            {
                if (StatsPlayer.stats.hp < StatsPlayer.stats.maxHP) {
                    StatsPlayer.stats.hp += 10;
                    StatsPlayer.stats.xp += enemyStats.xp;
                    HUD.Instance.ExpHandler(StatsPlayer.stats.xp);
                    HUD.Instance.HpAdd(StatsPlayer.stats.hp);
                } else if(StatsPlayer.stats.hp > StatsPlayer.stats.maxHP) {
                    StatsPlayer.stats.hp = StatsPlayer.stats.maxHP;
                }
            }
        }
    }
}

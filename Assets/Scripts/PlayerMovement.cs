using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Stats stats;
    public GameObject player;
    public float jumpForce = 5f;
    public float rotationDuration = 0.5f; // Duração do giro em segundos
    private Rigidbody2D rb;
    private bool is_jumping = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        stats.hp = stats.maxHP;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && is_jumping == false)
        {
            Jump();
        }

        if (stats.hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Jump()
    {
        stats.invincible = true;
        is_jumping = true;
        rb.velocity = Vector2.up * jumpForce; // Define a velocidade do pulo
        StartCoroutine(PerformFlip());
    }

    private IEnumerator PerformFlip()
    {
        float elapsed = 0f;
        float initialRotation = transform.eulerAngles.z; // Pega o ângulo inicial no eixo Z

        while (elapsed < rotationDuration)
        {
            float angle = Mathf.Lerp(0, 360f, elapsed / rotationDuration); // Rotação completa de 360°
            transform.rotation = Quaternion.Euler(0, 0, -1 * (initialRotation + angle)); // Aplica a rotação no eixo Z
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Garante que a rotação finalize exatamente em 360 graus a partir da rotação inicial
        transform.rotation = Quaternion.Euler(0, 0, initialRotation + 360f);
        is_jumping = false;
        stats.invincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Barrier" && collision.gameObject.tag != "Soul")
        {
            if (is_jumping)
            {
                Stats enemyStats = collision.gameObject.GetComponent<Enemies>().stats;
                UtilsFunc.TakeDamage(collision.gameObject, stats.baseAttack);

                if (enemyStats.hp <= 0)
                {
                    if (stats.hp < stats.maxHP)
                    {
                        stats.hp += 10;
                        HUD.Instance.HpAdd(stats.hp);
                      //  HUD.Instance.ExpHandler(enemyStats.xp);

                    }
                    else if (stats.hp > stats.maxHP)
                    {
                        stats.hp = stats.maxHP;
                    }
                }
            }

        }
        else if (collision.gameObject.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Barrier" && collision.gameObject.tag != "Soul")
        {
            if (Input.GetKeyDown(KeyCode.Space) && is_jumping)
            {
                Stats enemyStats = collision.gameObject.GetComponent<Enemies>().stats;

                // Aplica dano ao inimigo
                UtilsFunc.TakeDamage(collision.gameObject, stats.baseAttack);
                Debug.Log($"Dano aplicado ao inimigo: {stats.baseAttack}. HP do inimigo: {enemyStats.hp}");

                // Verifica se o inimigo morreu
                if (enemyStats.hp <= 0)
                {
                    // Regenera a vida do jogador se o HP estiver abaixo do máximo
                    if (stats.hp < stats.maxHP)
                    {
                        stats.hp += 10;  // Adicionei um log aqui para verificar a cura
                        HUD.Instance.HpAdd(stats.hp);
                       // HUD.Instance.ExpHandler(enemyStats.xp);
                        Debug.Log($"Jogador curado! Novo HP: {stats.hp}");
                    }
                }
            }
        }
    }

    public void ChangeToWhiteTemporarily(float duration)
    {
        StartCoroutine(WhiteEffect(duration));
    }
    private IEnumerator WhiteEffect(float duration)
    {
        spriteRenderer.color = Color.red; // Troca para vermelho temporariamente
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor; // Restaura a cor original
    }

}

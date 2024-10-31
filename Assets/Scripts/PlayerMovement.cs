using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public delegate void ActivePowerUp();
    public event ActivePowerUp OnActivatedPowerUp;
    public Stats stats;
    public GameObject player;
    public float jumpForce = 5f;
    public float rotationDuration = 0.5f; // Duração do giro em segundos
    private Rigidbody2D rb;
    private bool is_jumping = false;
    SpecialAttack special;
    private bool skill = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public AudioSource JumpAudio;
    public GameObject specialButton;

    void Start()
    {
        special = gameObject.GetComponent<SpecialAttack>();
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
            JumpAudio.Play();
        }

        if (stats.hp <= 0)
        {
            Destroy(gameObject);
        }
        specialButtonActivate();
        if(stats.xp == 100) {
            OnActivatedPowerUp?.Invoke();
            stats.xp = 0;
        }
    }

    private void Jump()
    {
        is_jumping = true;
        rb.velocity = Vector2.up * jumpForce; // Define a velocidade do pulo
        StartCoroutine(PerformFlip());
    }

    private IEnumerator PerformFlip()
    {
        stats.invincible = true;
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
        if (!special.is_tripled)
        {
            stats.invincible = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Medusa" || collision.gameObject.tag == "Pumpking" || collision.gameObject.tag == "Stalker" || collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Default")
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
                        stats.xp += enemyStats.xp;
                        HUD.Instance.ExpHandler(stats.xp);
                        HUD.Instance.HpAdd(stats.hp);

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
        if (collision.gameObject.tag == "Tank")
        {
            StartCoroutine(back(collision.gameObject));
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Barrier" && collision.gameObject.tag != "Soul")
        {
            if (is_jumping)
            {
                Stats enemyStats = collision.gameObject.GetComponent<Enemies>().stats;

                // Aplica dano ao inimigo
                UtilsFunc.TakeDamage(collision.gameObject, stats.baseAttack);

                // Verifica se o inimigo morreu
                if (enemyStats.hp <= 0)
                {
                    // Regenera a vida do jogador se o HP estiver abaixo do máximo
                    if (stats.hp < stats.maxHP)
                    {
                        stats.hp += 10;  // Adicionei um log aqui para verificar a cura
                        HUD.Instance.HpAdd(stats.hp);
                       // HUD.Instance.ExpHandler(enemyStats.xp);
                    }
                }
            }
        }
    }*/

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

    void specialButtonActivate()
    {
        if (stats.souls >= 100)
        {
            specialButton.SetActive(true);
        }
    }

    IEnumerator back(GameObject enemy)
    {

        float retreatTime = 0.5f;
        float elapsedTime = 0;

        // Recuo do inimigo para a direita
        while (elapsedTime < retreatTime)
        {
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0); // Aplique um recuo para a direita
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

}

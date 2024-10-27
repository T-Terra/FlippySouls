using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Stats stats;
    public GameObject player;
    public float jumpForce = 5f;
    public float rotationDuration = 0.5f; // Duração do giro em segundos
    private Rigidbody2D rb;
    private bool is_jumping = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        stats.hp = stats.maxHP;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && is_jumping == false)
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
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag != "Spell" && collision.gameObject.tag != "Barrier")
        {
            if (is_jumping)
            {

                Stats enemyStats = collision.gameObject.GetComponent<Enemies>().stats;
                UtilsFunc.TakeDamage(collision.gameObject, stats.baseAttack);

                if (enemyStats.hp <= 0)
                {
                    if (stats.hp < stats.maxHP)
                    {
                        stats.hp += 1;
                    }
                    else if (stats.hp > stats.maxHP)
                    {
                        stats.hp = stats.maxHP;
                    }
                }
            }

        }else if (collision.gameObject.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
}

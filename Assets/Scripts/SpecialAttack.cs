using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float size_grow = 3f; // Fator de crescimento ao ativar o especial
    public float special_attack_timer = 5f; // Tempo mínimo entre especiais
    public float timelimit = 3f; // Duração do especial
    public float rotationSpeed = 360f; // Velocidade de rotação em graus por segundo

    private Transform player_transform;
    private Stats player_stats;
    private PlayerMovement player_;
    private Vector3 originalScale; // Escala original do player
    private Quaternion originalRotation; // Rotação original do player

    private float timer = 0f;
    public bool is_tripled = false;
    private float t0 = 0f;
    public AudioSource SpecialAudio;
    public GameObject specialButton;

    void Start()
    {
        // Encontrar o player e componentes necessários
        player_transform = gameObject.GetComponent<Transform>();
        player_stats = gameObject.GetComponent<PlayerMovement>().stats;
        player_ = gameObject.GetComponent<PlayerMovement>();

        // Armazena o tamanho e rotação original do player
        originalScale = player_transform.localScale;
        originalRotation = player_transform.rotation;
    }

    void Update()
    {
        // Atualiza o temporizador
        timer += Time.deltaTime;

        // Ativa o especial se o tempo mínimo e a tecla R forem satisfeitos
        if (Input.GetKeyDown(KeyCode.R) && player_stats.souls >= 100 && !is_tripled)
        {
            t0 = timer;
            ActivateSpecialAttack();
        }

        // Desativa o especial quando o tempo limite é alcançado
        if (is_tripled && timer >= (t0 + timelimit))
        {
            StartCoroutine(DeactivateSpecialAttack());
        }
    }

    public void ActivateSpecialMobile() {
        timer += Time.deltaTime;

        ActivateSpecialAttackMobile();


        // Desativa o especial quando o tempo limite é alcançado
        if (is_tripled && timer >= (t0 + timelimit))
        {
            StartCoroutine(DeactivateSpecialAttack());
        }
    }

    public void ActivateSpecialAttack()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        SpecialAudio.Play();
        player_stats.invincible = true; // Torna o player invencível
        player_transform.localScale = originalScale * size_grow; // Triplica o tamanho do player
        player_.radius = 4.8f;
        is_tripled = true;
        specialButton.SetActive(false);
        player_stats.souls = 0f;
        // Inicia a rotação do especial
        StartCoroutine(PerformFlip());
    }

    public void ActivateSpecialAttackMobile()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        t0 = timer;
        SpecialAudio.Play();
        player_stats.invincible = true; // Torna o player invencível
        player_transform.localScale = originalScale * size_grow; // Triplica o tamanho do player
        player_.radius = 4.8f;
        is_tripled = true;
        specialButton.SetActive(false);
        player_stats.souls = 0f;
        // Inicia a rotação do especial
        StartCoroutine(PerformFlip());
    }

    private IEnumerator DeactivateSpecialAttack()
    {
        player_transform.localScale = originalScale; // Retorna ao tamanho original
        player_transform.rotation = originalRotation; // Retorna à rotação original
        player_.radius = 2f;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        is_tripled = false;
        timer = 0f; // Reinicia o timer
        
        this.gameObject.GetComponent<PlayerMovement>().stats.souls = 0; // reinicia as almas depois que acabar o especial
        yield return new WaitForSeconds(1f);
        player_stats.invincible = false;
    }

    private IEnumerator PerformFlip()
    {
        while (is_tripled)
        {
            // Rotaciona o personagem continuamente no eixo Z
            player_transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            yield return null;
        }

        // Garante que a rotação volte ao estado original ao final do especial
        player_transform.rotation = originalRotation;
    }
}

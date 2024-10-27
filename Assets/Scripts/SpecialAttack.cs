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
    private Vector3 originalScale; // Escala original do player
    private Quaternion originalRotation; // Rotação original do player

    private float timer = 0f;
    private bool is_tripled = false;
    private float t0 = 0f;

    void Start()
    {
        // Encontrar o player e componentes necessários
        player_transform = gameObject.GetComponent<Transform>();
        player_stats = gameObject.GetComponent<PlayerMovement>().stats;

        // Armazena o tamanho e rotação original do player
        originalScale = player_transform.localScale;
        originalRotation = player_transform.rotation;
    }

    void Update()
    {
        // Atualiza o temporizador
        timer += Time.deltaTime;

        // Ativa o especial se o tempo mínimo e a tecla R forem satisfeitos
        if (Input.GetKeyDown(KeyCode.R) && timer >= special_attack_timer && !is_tripled)
        {
            t0 = timer;
            ActivateSpecialAttack();
        }

        // Desativa o especial quando o tempo limite é alcançado
        if (is_tripled && timer >= (t0 + timelimit))
        {
            DeactivateSpecialAttack();
        }
    }

    void ActivateSpecialAttack()
    {
        player_transform.localScale = originalScale * size_grow; // Triplica o tamanho do player
        player_stats.invincible = true; // Torna o player invencível
        is_tripled = true;

        // Inicia a rotação do especial
        StartCoroutine(PerformFlip());
    }

    void DeactivateSpecialAttack()
    {
        player_transform.localScale = originalScale; // Retorna ao tamanho original
        player_transform.rotation = originalRotation; // Retorna à rotação original
       player_stats.invincible = false; // Remove a invencibilidade
        is_tripled = false;
        timer = 0f; // Reinicia o timer
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

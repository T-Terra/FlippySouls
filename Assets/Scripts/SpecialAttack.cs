using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float size_grow; // Fator de crescimento ao ativar o especial
    private GameObject player;
    public GameObject background;
    public GameObject enemies;
    private Stats player_stats;
    public float timer = 0;
    public float special_attack_timer = 5; // Tempo mínimo entre especiais
    private Transform player_transform;
    public float timelimit; // Duração do especial
    private bool is_tripled = false;
    public float t0 = 0;
    private Vector3 originalScale;
    public float rotationDuration = 0.5f; // Duração do giro em segundos
    private Quaternion originalRotation;

    void Start()
    {
        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Stats player_stats = player.stats;
        Scenario background = gameObject.GetComponentInChildren<Scenario>();
        
        player_transform = gameObject.GetComponent<Transform>();
        originalScale = player_transform.localScale;
        originalRotation = player_transform.rotation;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.R) && timer >= special_attack_timer && !is_tripled)
        {
            t0 = timer;
            specialAttack();
            
        }

        if (is_tripled && timer >= (t0 + timelimit))
        {
            deactivateSpecialAttack();
           
        }
    }

    void specialAttack()
    {   
        player_transform.localScale = originalScale * size_grow; // Triplica o tamanho
        player_stats.invincible = true;
        StartCoroutine(PerformFlip());
        is_tripled = true;

    }   

    void deactivateSpecialAttack()
    {   
        player_transform.localScale = originalScale; // Retorna ao tamanho original
        player_stats.invincible = false;
        StopCoroutine(PerformFlip());
    }

    private IEnumerator PerformFlip()
    {
        float elapsed = 0f;
        float initialRotation = transform.eulerAngles.z; // Pega o ângulo inicial no eixo Z

        while (is_tripled == true)
        {
            float angle = Mathf.Lerp(0, 360f, elapsed / rotationDuration); // Rotação completa de 360°
            transform.rotation = Quaternion.Euler(0, 0, -1 * (initialRotation + angle)); // Aplica a rotação no eixo Z
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}


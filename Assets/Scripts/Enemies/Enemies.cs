using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemies : MonoBehaviour
{
    Rigidbody2D rb;
    public Stats stats;
    public GameObject projectile;
    public GameObject soul;
    public Enemy[] enemyDataList;

    private GameObject player;
    private Stats playerStats;

    private Vector2 target;
    private Vector2 direction;

    private float startYPosition;
    private float time = 0f;
    private bool canAttack = false;

    private bool flip = false;
    private bool flipped = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startYPosition = transform.position.y;
        time = 0f;

        if (gameObject.CompareTag("Tank") || gameObject.CompareTag("Stalker"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player)
            {
                target = player.transform.position;
                direction = ((target - (Vector2)transform.position).normalized) * stats.speed;
                playerStats = player.GetComponent<PlayerController>().stats;

                if (gameObject.CompareTag("Tank"))
                {
                    stats.maxHP = playerStats.baseAttack * 2;
                }
            }
            else
            {
                direction = new Vector2(-stats.speed, 0);
            }
        }

        stats.hp = stats.maxHP;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Stats playerStats = collision.gameObject.GetComponent<Stats>();
            if (!playerStats.invincible)
            {
                playerStats.hp -= stats.baseAttack;
                UtilsFunc.TakeDamage(collision.gameObject, stats.baseAttack);
            }
        }
        else if (collision.gameObject.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        // Define o movimento do inimigo com base na sua tag
        switch (gameObject.tag)
        {
            case "Medusa":
                float range = 3f; // Amplitude do zig-zag
                float zigZagFrequency = 3f; // Frequência fixa do zig-zag
                float zigZagMoviment = Mathf.Sin(Time.time * zigZagFrequency) * range;

                rb.velocity = new Vector2(-stats.speed, zigZagMoviment);
                break;

            case "Pumpking":
                rb.velocity = new Vector2(-stats.speed, 0);
                if (canAttack)
                {
                    Vector2 spawnPosition = new Vector2(transform.position.x - 1, transform.position.y);
                    GameObject spell = Instantiate(projectile, spawnPosition, Quaternion.identity);

                    SpellScript spellScript = spell.GetComponent<SpellScript>();
                    spellScript.speed = stats.speed * 3;
                    spellScript.damage = stats.baseAttack;

                    time = 0f;
                }
                break;

            case "Stalker":
                rb.velocity = direction;
                break;

            default:
                Debug.Log("Default");
                Debug.Log(time);
                if (flip && !flipped)
                {
                    SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                    spriteRenderer.flipX = true;
                    stats.speed -= 1;
                    flipped = true;
                }
                rb.velocity = new Vector2(-stats.speed, 0);
                break;
        }

        if (stats.hp <= 0)
        {
            DeathSequence();
        }

        //Verificação de cooldown
        if (time < stats.attackSpeed)
        {
            canAttack = false;
            time += Time.fixedDeltaTime;
        }
        else
        {
            canAttack = true;
        }

        if (time > 5f && !flipped)
        {
            flip = true;
        }

    }

    IEnumerator DeathSequence()
    {
        Vector2 retreatPosition = transform.position - new Vector3(0.5f, 0); // Ajuste o valor para recuar mais ou menos
        float retreatTime = 0.2f; // Tempo de recuo

        float elapsedTime = 0;
        while (elapsedTime < retreatTime)
        {
            transform.position = Vector2.Lerp(transform.position, retreatPosition, elapsedTime / retreatTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Drop do item
        if (soul)
        {
            Instantiate(soul, transform.position, Quaternion.identity);
        }

        // Destruir o inimigo
        Destroy(gameObject);
    }
}

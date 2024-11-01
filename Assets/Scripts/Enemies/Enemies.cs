using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemies : MonoBehaviour
{
    Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
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

    /*private bool flip = false;
    private bool flipped = false;*/
    bool death = false;

    private Color originalColor;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerMovement>().stats;
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        startYPosition = transform.position.y;
        time = 0f;

        if (gameObject.CompareTag("Tank") || gameObject.CompareTag("Stalker"))
        {
            if (player)
            {
                target = player.transform.position;
                direction = ((target - (Vector2)transform.position).normalized) * stats.speed;
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
            PlayerMovement playerStats = collision.gameObject.GetComponent<PlayerMovement>();
            if (!playerStats.stats.invincible)
            {
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
        if (!death)
        {
            switch (gameObject.tag)
            {
                case "Medusa":
                    float range = 3f; // Amplitude do zig-zag
                    float zigZagFrequency = 3f; // Frequ�ncia fixa do zig-zag
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
                    /* if (flip && !flipped)
                     {
                         SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                         spriteRenderer.flipX = true;
                         stats.speed -= stats.speed * 0.7f;
                         flipped = true;
                     } */
                    rb.velocity = new Vector2(-stats.speed, 0);
                    break;
            }
        }

        if (stats.hp <= 0)
        {
            if (!death)
            {
                StartCoroutine(DeathSequence());
            }
        }

        //Verifica��o de cooldown
        if (time < stats.attackSpeed)
        {
            canAttack = false;
            time += Time.fixedDeltaTime;
        }
        else
        {
            canAttack = true;
        }

        /*  if (time > 1f && !flipped)
          {
              flip = true;
          }
        */

    }

    IEnumerator DeathSequence()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        death = true;  // Marque que o inimigo est� em processo de morte

        float retreatTime = 0.5f;
        float elapsedTime = 0;

        // Recuo do inimigo para a direita
        while (elapsedTime < retreatTime)
        {
            rb.velocity = new Vector2(5f, 0); // Aplique um recuo para a direita
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pare o movimento ap�s o recuo
        rb.velocity = Vector2.zero;

        // Drop do item
        if (soul)
        {
            Instantiate(soul, transform.position, Quaternion.identity);
        }

        // Destruir o inimigo
        Destroy(gameObject);
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
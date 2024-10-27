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
                direction = new Vector2(-stats.speed, startYPosition);
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
                float range = 3f;
                float zigZagMoviment = startYPosition + Mathf.Sin(Time.time * (3 + (stats.speed * 0.1f))) * range;
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
                rb.velocity = new Vector2(-stats.speed, 0);
                break;
        }

        //Verificação de vida
        if (stats.hp <= 0)
        {
            if (soul)
            {
                Instantiate(soul, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }
            Destroy(gameObject);
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

    }
}

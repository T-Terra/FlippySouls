using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyBase;
    public Enemy[] listEnemies = null;
    private float playTime = 0f;
    private float timer = 0f;
    public float cooldown = 5f;
    public float minCooldown = 1f;
    public float range = 60f;

    void Start()
    {

    }

    private void FixedUpdate()
    {
        playTime += Time.fixedDeltaTime;
        timer += Time.fixedDeltaTime;

        if (playTime > range)
        {
            playTime = 0f;
            cooldown = Mathf.Max(cooldown * 0.95f , minCooldown);
        }

        if (timer > cooldown)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        Enemy enemyData = listEnemies[Random.Range(0, listEnemies.Length)];

        Camera mainCamera = Camera.main;
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect * 2;
        float screenHeight = mainCamera.orthographicSize * 2;

        float randomY = Random.Range(-screenHeight / 2 + 2, screenHeight / 2 -2);
        Vector2 spawnPosition = new Vector2(screenWidth / 2 + 2, randomY);

        GameObject enemy = Instantiate(enemyBase, spawnPosition, Quaternion.identity);
        if(enemyData != null) 
        { 
            enemy.GetComponent<Enemies>().stats = enemyData.stats;
            enemy.GetComponent<SpriteRenderer>().sprite = enemyData.sprite;
            enemy.tag = enemyData.id;
        }

        Debug.Log("Nasceu: " + enemyData.id);
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            cooldown = Mathf.Max(cooldown * 0.95f, minCooldown);
        }

        if (timer > cooldown)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        int qntSpawn = Random.Range(1, 3);

        Camera mainCamera = Camera.main;
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect * 2;
        float screenHeight = mainCamera.orthographicSize * 2;

        // Chama a coroutine para spawnar inimigos
        StartCoroutine(SpawnEnemies(qntSpawn, screenWidth, screenHeight));
    }
    IEnumerator SpawnEnemies(int qntSpawn, float screenWidth, float screenHeight)
    {
        for (int i = 0; i < qntSpawn; i++)
        {
            Enemy enemyData = listEnemies[Random.Range(0, listEnemies.Length)];
            float randomY;
            if (enemyData.id == "Medusa")
            {
                randomY = Random.Range(-screenHeight / 2 + 3, screenHeight / 2 - 3);
            }
            else
            {
                randomY = Random.Range(-screenHeight / 2 + 0.5f, screenHeight / 2 - 1f);
            }

            Vector2 spawnPosition = new Vector2(screenWidth / 2 + 2, randomY);

            GameObject enemy = Instantiate(enemyBase, spawnPosition, Quaternion.identity);
            if (enemyData != null)
            {
                if (enemyData.id != "Default")
                {
                    enemy.tag = enemyData.id;
                }

                enemy.GetComponent<Enemies>().stats = enemyData.stats.Clone();
                enemy.GetComponent<SpriteRenderer>().sprite = enemyData.sprite;
            }

            yield return new WaitForSeconds(0.5f); // Delay de 0.1 segundos entre cada spawn
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulScript : MonoBehaviour
{
    HUD hud;

    void Update()
    {
        transform.position += Vector3.left * 2 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = gameObject.GetComponent<PlayerController>();
            player.stats.souls += 1;
          //  hud.SoulsHandler(player.stats.souls);
            Destroy(gameObject);
        }else if (collision.gameObject.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
}

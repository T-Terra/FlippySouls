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
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.stats.souls += 15;
            HUD.Instance.ExpHandler(15);
            HUD.Instance.SoulsHandler(player.stats.souls);
            //  hud.SoulsHandler(player.stats.souls);
            Destroy(gameObject);
        }else if (collision.gameObject.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
}

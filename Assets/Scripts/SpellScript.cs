using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class SpellScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float damage;
    public GameObject target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(- speed, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            UtilsFunc.TakeDamage(collision.gameObject, damage);
        }
        else if(collision.gameObject.tag == "Barrier")
        {
            Destroy(gameObject);
        }
    }

}

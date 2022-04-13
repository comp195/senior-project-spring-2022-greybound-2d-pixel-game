using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Prefab shooting */

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 20;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyAI enemy = hitInfo.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            //enemy.TakeDamage(10);
        }

        /* If wanted to add effect then add into unity inspector*/
        Instantiate(impactEffect, transform.position, transform.rotation);

        //Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }
}

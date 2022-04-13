using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public AIPath aiPath;
    public Animator body;

    //public GameObject enemy1;
    //public GameObject enemy2;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        HealthStat();
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
        
    }

    void Animate()
    {
        /*
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        */

        body.SetFloat("Horizontal", aiPath.desiredVelocity.x);
        body.SetFloat("Vertical", aiPath.desiredVelocity.y);
        body.SetFloat("Magnitude", aiPath.desiredVelocity.magnitude);
    }

    public void HealthStat()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage (int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void SaveEnemy()
    {
        SaveSystem.SaveEnemy(this);
    }


    public void LoadEnemy()
    {
        PlayerData data = SaveSystem.LoadEnemy();

        /* Import all player attributes */
        currentHealth = data.health;
        healthBar.SetHealth(currentHealth);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}

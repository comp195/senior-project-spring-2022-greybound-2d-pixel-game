using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserController2D : MonoBehaviour
{
    public Animator topAnimator;
    public Animator bottomAnimator;
    //public GameObject crossHair;

    public string currentScene;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    Vector3 movement;

    void Start()
    {
        HealthStat();
    }

    // Start is called before the first frame update
    void Update()
    {
        ProcessInputs();
        TopAnimate();
        BottomAnimate();
        Move();
    }

    void ProcessInputs()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            HealDamage(20);
        }
    }

    public void Move()
    {
        /* Smooths movement out */
        transform.position = transform.position + movement * Time.deltaTime;
    }

    public void TopAnimate()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        //movingAnimator.SetFloat("Horizontal", movement.x);
        topAnimator.SetFloat("Horizontal", aimDirection.x);
        //movingAnimator.SetFloat("Vertical", movement.y);
        topAnimator.SetFloat("Vertical", aimDirection.y);
        //movingAnimator.SetFloat("Magnitude", movement.magnitude);
        topAnimator.SetFloat("Magnitude", aimDirection.magnitude);
    }

    public void BottomAnimate()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        bottomAnimator.SetFloat("Horizontal", aimDirection.x);
        bottomAnimator.SetFloat("Vertical", aimDirection.y);
        bottomAnimator.SetFloat("Magnitude", movement.magnitude);
    }

    void HealthStat()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    
    void HealDamage(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }


    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currentScene = data.scene;
        /*
        if (currentScene == "TestLevel")
        {
            SceneManager.LoadScene("TestLevel");
        }

        if (currentScene == "Saloon")
        {
            SceneManager.LoadScene("Saloon");
        }
        /*

        /* Import all player attributes */
        currentHealth = data.health;
        healthBar.SetHealth(currentHealth);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        /* Destroy bullets when loading */

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
        }
    }

    /* Get Mouse Position in World with z = 0f */
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}

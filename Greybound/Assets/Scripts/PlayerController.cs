/* Windows Input Manager */
// mouse 0 = left click
// mouse 1 = right click

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator shootingAnimator;
    //public Animator movementAnimator;
    public GameObject crossHair;
    public GameObject bulletPrefab;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public string currentScene;

    Vector3 movement;
    Vector3 mouseMovement;
    Vector3 aiming;

    /*
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    */

    void Start()
    {
        
        HealthStat();

        Scene scene = SceneManager.GetActiveScene();
        currentScene = scene.name;
        //Debug.Log(currentScene);
    }

    void Update()
    {
        /* Vector for movement */
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        ProcessInputs();
        AimAndShoot();
        Animate();
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            HealDamage(20);
        }

        /*
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        */

        /* Smooths movement out */
        //transform.position = transform.position + movement * Time.deltaTime;
    }

    private void ProcessInputs()
    {
        /* Vector for movement */
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        /* Vector for mouse movement */
        mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);

        aiming = aiming + mouseMovement;

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aiming.Normalize();
        }

        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }
    }

    public void Move()
    {
        /* Smooths movement out */
        transform.position = transform.position + movement * Time.deltaTime;
    }

    public void Animate()
    {
        //movementAnimator.SetFloat("Horizontal", movement.x);
        //movementAnimator.SetFloat("Vertical", movement.y);
        //movementAnimator.SetFloat("Magnitude", movement.magnitude);

        shootingAnimator.SetFloat("Horizontal", aiming.x);
        shootingAnimator.SetFloat("Vertical", aiming.y);
        shootingAnimator.SetFloat("Magnitude", aiming.magnitude);
    }

    public void AimAndShoot()
    {
        //Vector3 aim = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector2 shootingDirection = new Vector2(aiming.x, aiming.y);

        //if (aim.magnitude > 0.0f)
        if (Input.GetButton("Aim"))
        {
            /* Makes it closer */
            crossHair.transform.localPosition = aiming * 0.4f;
            crossHair.SetActive(true);
            RemoveCursor();

            shootingDirection.Normalize();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                /* Multiply by value to increase speed */
                bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * 3.0f;
                bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                /* Destroy bullet x seconds after shooting */
                Destroy(bullet, 2.0f);
                //Debug.Log("Fire!");
            }

        } else
        {
            crossHair.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void RemoveCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
}



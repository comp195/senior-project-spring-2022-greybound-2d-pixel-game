/* Windows Input Manager */
// mouse 0 = left click
// mouse 1 = right click

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameObject crossHair;
    public GameObject bulletPrefab;

    Vector3 movement;
    Vector3 aim;
    bool isAiming;
    bool endOfAiming;

    
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        /* Vector for movement */
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        ProcessInputs();
        AimAndShoot();
        Animate();
        Move();

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
        Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
        aim = aim + mouseMovement;
        /* Restricts crosshair x units away from player */
        if (aim.magnitude > 1.0f)
        {
            aim.Normalize();
        }
        //aim = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        isAiming = Input.GetKeyDown(KeyCode.Mouse0);
        endOfAiming = Input.GetKeyUp(KeyCode.Mouse0);

        if(movement.magnitude > 1.0f)
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
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
    }

    public void AimAndShoot()
    {
        //Vector3 aim = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();

            /* Makes it closer */
            // Adjust value for different ranges
            aim *= 0.4f;
            crossHair.transform.localPosition = aim;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (endOfAiming)
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

        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        /* Import all player attributes */
        
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        /* Destroy bullets when loading */
        
    }
}



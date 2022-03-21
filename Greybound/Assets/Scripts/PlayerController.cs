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

    // Update is called once per frame
    void Update()
    {
        /* Vector for movement */
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);



        AimAndShoot();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        /* Smooths movement out */
        transform.position = transform.position + movement * Time.deltaTime;
    }

    private void ProcessInputs()
    {

    }

    public void AimAndShoot()
    {
        Vector3 aim = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector2 shootingDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();

            /* Makes it closer */
            // Adjust value for different ranges
            //aim *= 0.4f;
            crossHair.transform.localPosition = aim;
            crossHair.SetActive(true);

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



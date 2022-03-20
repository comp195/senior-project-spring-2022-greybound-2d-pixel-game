/* Windows Input Manager */
// mouse 0 = left click
// mouse 1 = right click

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{

    public Animator topAnimator;
    public Animator bottomAnimator;
    public GameObject crossHair;

    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        /* Vector for movement */
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector3 aim = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);



        AimAndShoot();

        bottomAnimator.SetFloat("Horizontal", movement.x);
        bottomAnimator.SetFloat("Vertical", movement.y);
        bottomAnimator.SetFloat("Magnitude", movement.magnitude);

        topAnimator.SetFloat("MoveHorizontal", movement.x);
        topAnimator.SetFloat("MoveVertical", movement.y);
        topAnimator.SetFloat("MoveMagnitude", movement.magnitude);

        topAnimator.SetFloat("AimHorizontal", aim.x);
        topAnimator.SetFloat("AimVertical", aim.y);
        topAnimator.SetFloat("AimMagnitude", aim.magnitude);
        topAnimator.SetBool("Aim", Input.GetButtonDown("FireMouse"));

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
            if (Input.GetButtonUp("FireMouse"))
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
}



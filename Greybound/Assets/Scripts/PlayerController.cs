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

        if(Input.GetButtonDown("FireMouse"))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            /* Change x value in Vector2 for velocity speed */
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, 0.0f);
            //Debug.Log("Fire!");
        }

        MoveCrosshair();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        /* Smooths movement out */
        transform.position = transform.position + movement * Time.deltaTime;
    }

    private void ProcessInputs()
    {

    }

    public void MoveCrosshair()
    {
        Vector3 aim = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        if(aim.magnitude > 0.0f)
        {
            aim.Normalize();

            /* Makes it closer */
            // Adjust value for different ranges
            //aim *= 0.4f;
            crossHair.transform.localPosition = aim;
            crossHair.SetActive(true);
        } else
        {
            crossHair.SetActive(false);

        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMovement : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Horizontal"));

        /* Vector for (left & right) movement */
        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);

        /* Vector for (up & down) movement */
        Vector3 vertical = new Vector3(0.0f, Input.GetAxis("Vertical"), 0.0f);

        /* Smooths movement out */
        transform.position = transform.position + horizontal * Time.deltaTime;
        transform.position = transform.position + vertical * Time.deltaTime;
    }
}

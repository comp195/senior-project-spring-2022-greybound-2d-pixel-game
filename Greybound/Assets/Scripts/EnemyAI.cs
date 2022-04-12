using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public AIPath aiPath;
    public Animator body;

    // Update is called once per frame
    void Update()
    {
        Animate();
        aiPath.canMove(false);
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
}

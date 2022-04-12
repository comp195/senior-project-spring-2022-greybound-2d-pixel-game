using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] private UserAimWeapon userAimWeapon;

    private void Start()
    {
        userAimWeapon.OnShoot += userAimWeapon_OnShoot;
    }

    private void userAimWeapon_OnShoot(object sender, UserAimWeapon.OnShootEventArgs e)
    {
        /* Insert Animations */

    }
}

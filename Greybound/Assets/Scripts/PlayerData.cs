using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    /* Add more stats later */
    //public int level;
    public int health;
    public string scene;
    public float[] position;

    public PlayerData (PlayerController player)
    {
        /* Import all player attributes */
        health = player.currentHealth;
        scene = player.currentScene;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

}

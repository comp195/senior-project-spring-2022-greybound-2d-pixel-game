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

    public PlayerData (UserController2D player)
    {
        /* Import all player attributes */
        health = player.currentHealth;
        scene = player.currentScene;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }    
    
    public PlayerData (EnemyAI enemy)
    {
        /* Import all player attributes */
        health = enemy.currentHealth;
        //scene = enemy.currentScene;

        position = new float[3];
        position[0] = enemy.transform.position.x;
        position[1] = enemy.transform.position.y;
        position[2] = enemy.transform.position.z;
    }

}

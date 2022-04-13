using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Playgame()
    {
        /* Loads scence from build order (-> Game is index 1)*/
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Prologue");
    }

    public void Quitgame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Loadgame()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        /* Import all player attributes */

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        SceneManager.LoadScene("Prologue");
    }
}

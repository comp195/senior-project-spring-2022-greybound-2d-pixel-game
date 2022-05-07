using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene2D : MonoBehaviour
{
    [SerializeField] private string newLevel;
    public GameObject spawnPoint;
    public string currentScene;
    public float loadRate = 5f;
    float nextLoadTime = 10f;

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        currentScene = scene.name;
        //LoadingScreen();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene(newLevel);

        }
    }


    void LoadingScreen()
    {
        if (currentScene == "LoadingScreen")
        {
            if (Time.time >= nextLoadTime)
            {
                nextLoadTime = Time.time + 1f / loadRate;
                //SceneManager.LoadScene("Prologue");
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Bye : MonoBehaviour
{

    public static float timeToStop = 0;
    public float endTimer = 300;

    Scene current_Scene;

    void Start()
    {

    }

    private void FixedUpdate()
    {
        timeToStop = timeToStop + Time.deltaTime;

        if (timeToStop >= endTimer)
        {
            LoadNextLevel();
        }

    }

    public void LoadNextLevel()
    {
        {
            SceneManager.LoadScene("Bye");
        }

    }

}

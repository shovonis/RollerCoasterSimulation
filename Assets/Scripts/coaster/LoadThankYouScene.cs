using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadThankYouScene : MonoBehaviour
{
    public static float timeToStop;
    public float endTimer = 890;

    private void FixedUpdate()
    {
        timeToStop = timeToStop + Time.deltaTime;
        bool loadNextScene = Input.GetKeyDown(KeyCode.N);
        if (timeToStop >= endTimer || loadNextScene)
        {
            if (loadNextScene)
            {
                Logger.Log(LogLevel.INFO,
                    "Next key pressed. Terminating roller coaster moving scene.");
            }
            else
            {
                Logger.Log(LogLevel.INFO, "Coaster simulation finished. Total runtime: " + endTimer + "s");
                Logger.Log(LogLevel.INFO, "Starting last static scene...");
            }

            SceneManager.LoadScene("Thank_You_Screen");
        }
    }
}
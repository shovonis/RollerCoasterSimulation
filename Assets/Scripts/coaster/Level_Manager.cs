using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour {

    public static float timeToStop = 0;
    public float endTimer = 300;

    Scene current_Scene;
    //Scene Start_Screen;
    //Scene Coaster_Simulation;
    //Scene Thank_You_Screen;

	// Use this for initialization
	void Start () {
        current_Scene = SceneManager.GetActiveScene();

    }

    private void FixedUpdate()
    {
        timeToStop = timeToStop + Time.deltaTime;

        if (timeToStop >= endTimer)
        {
            LoadNextLevel();
        }

    }

    /*
    // Update is called once per frame
    void FiUpdate () {
        timeToStop = timeToStop + Time.deltaTime;

        if (timeToStop >= 20) {
            LoadNextLevel();
        }
		
	}*/

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex <= 2)
        {
            //Destroy(GameObject.FindObjectOfType<>());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else Application.Quit();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

   }

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public float endTimer = 300;
    private Scene currentScene;
    public static float timeToStop;
    private float timeForNextExperiment;
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        timeToStop = timeToStop + Time.deltaTime;
        timeForNextExperiment = timeForNextExperiment + Time.deltaTime;
        bool loadNextScene = Input.GetKeyDown(KeyCode.N);
        
        if (timeToStop >= endTimer || loadNextScene)
        {
            NextScene(loadNextScene);
        }   
    }
    
    private void NextScene(bool loadNextScene)
    {
        if (loadNextScene)
        {
            Logger.Log(LogLevel.INFO,
                "Next key pressed. Terminating initial static scene.");
        }
        else
        {
            Logger.Log(LogLevel.INFO, "Initial static Scene finished. Total runtime: " + endTimer + "s");
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

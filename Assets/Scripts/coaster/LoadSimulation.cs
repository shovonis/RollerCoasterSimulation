using UnityEngine;
 using UnityEngine.SceneManagement;
 
 public class LoadSimulation : MonoBehaviour
 {
     public float endTimer = 300;
     private Scene currentScene;
     public static float timeToStop;
 
     private void FixedUpdate()
     {
         timeToStop = timeToStop + Time.deltaTime;
         bool loadNextScene = Input.GetKeyDown(KeyCode.N);
         if (timeToStop >= endTimer || loadNextScene)
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
 
             SceneManager.LoadScene("Coaster_Simulation");
         }
     }
 }
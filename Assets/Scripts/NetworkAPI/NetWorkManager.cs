using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace NetworkAPI
{
    public class NetWorkManager : MonoBehaviour
    {
        [SerializeField] private int dataCollectionFreq = 60;
        [SerializeField] private float endTimer = 300;

        private Scene _currentScene;
        private float _timerForGettingData;
        private static float _simulationTime;
        private static string CYBER_SENSE_SERVER_URL = "http://localhost:5555/GetPrediction";
        public float PredictedCyberSickness { get; set; } = -10.0f;
        private void FixedUpdate()
        {
            _simulationTime = _simulationTime + Time.deltaTime;
            _timerForGettingData = _timerForGettingData + Time.deltaTime;
            
            if (_timerForGettingData >= dataCollectionFreq)
            {
                Debug.Log("Timer expired! Sending cyber sickness GET request to server..");
                StartCoroutine(GetCybersicknessPrediction(CYBER_SENSE_SERVER_URL));
                _timerForGettingData = 0.0f; // Resetting timer
            }

            if (_simulationTime >= endTimer)
            {
                TerminateSimulation();
            }
        }
        IEnumerator GetCybersicknessPrediction(string uri)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
                PredictedCyberSickness = -10.0f;;
            }
            else
            {
                Debug.Log("Received Cybersickness Prediction: " + uwr.downloadHandler.text);
                PredictedCyberSickness = float.Parse(uwr.downloadHandler.text);
            }
        }


        private void TerminateSimulation()
        {
           int sceneIndex =  SceneManager.GetActiveScene().buildIndex;
           SceneManager.UnloadSceneAsync(sceneIndex);
        }
        private void LoadNextSceneAndStopExp(bool loadNextScene)
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
}
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetworkAPI
{
    public class NetWorkManager : MonoBehaviour
    {
        [SerializeField] private String _experimentName;
        private bool hasExperimentStarted;
        public float endTimer = 300;
        private Scene currentScene;
        public static float timeToStop;
        private float timeForNextExperiment;
        private PythonServerAPI _pythonServerApi;
        void Start()
        {
            _pythonServerApi = new PythonServerAPI();
            _pythonServerApi.Start();
            hasExperimentStarted = false;
            if (!hasExperimentStarted)
            {
                SendStartExperimentReq(_experimentName);
            }
        }

        private void FixedUpdate()
        {
            timeToStop = timeToStop + Time.deltaTime;
            timeForNextExperiment = timeForNextExperiment + Time.deltaTime;
            bool loadNextScene = Input.GetKeyDown(KeyCode.N);

            if (timeToStop >= endTimer && _pythonServerApi.hasDataProcessed)
            {
                LoadNextSceneAndStopExp(loadNextScene);
            }
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

            CloseSeverConnection();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void SendStartExperimentReq(String expName)
        {
            _pythonServerApi.SetMessageAndSend(expName);
            hasExperimentStarted = true;
        }

        private void CloseSeverConnection()
        {
            _pythonServerApi.hasDataProcessed = false;
            _pythonServerApi?.Stop();
        }
    }
}
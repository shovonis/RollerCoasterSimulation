using UnityEngine;

public class LogStartTime : MonoBehaviour
{
    void Start()
    {
        Logger.Log(LogLevel.INFO, "----------------new simulation-----------------");
        Logger.Log(LogLevel.INFO, "Roller coaster first static start scene started.");
    }

    private void OnApplicationQuit()
    {
        Logger.Log(LogLevel.INFO, "Simulation Done! Application is quiting");
    }
}
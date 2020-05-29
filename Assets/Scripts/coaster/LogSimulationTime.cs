using UnityEngine;

public class LogSimulationTime : MonoBehaviour
{
    void Start()
    {
        Logger.Log(LogLevel.INFO, "Roller coaster moving scene started.");
    }

    private void OnApplicationQuit()
    {
        Logger.Log(LogLevel.INFO, "Simulation Done! Application is quiting");
    }
}
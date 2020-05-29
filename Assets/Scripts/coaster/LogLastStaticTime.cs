using UnityEngine;

public class LogLastStaticTime : MonoBehaviour {

	public static float timeToStop;
	public float endTimer = 10; 
	void Start()
	{
		Logger.Log(LogLevel.INFO, "Roller coaster last static start scene started.");
	}
	
	private void OnApplicationQuit()
	{
		Logger.Log(LogLevel.INFO, "Total simulation done! Application is quiting.");
	}
}

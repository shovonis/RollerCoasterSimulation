using UnityEngine;

public class LogLoopTime : MonoBehaviour
{
    private int loop = 1;

    private void OnTriggerEnter(Collider other)
    {
        Logger.Log("INFO", "Loop " + loop + " completed. Verbal Feedback taken.");
        loop++;
    }
}
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class data_Collection : MonoBehaviour
{
    private int clickCounter;
    private int left_clickCounter;
    private int right_clickcounter;
    private int middle_clickcounter;
    private StringBuilder strigBuilder = new StringBuilder();
    private Boolean RequestedToQuit;
    public Boolean quitOnClick;
    public float timeToQuit;
    private float timeCountedToQuit;

    private Vector3 xyzVel;
    private Vector3 rotVel;
    private Vector3 lastRot;
    private Vector3 lastPos;
    private Vector3 localRot;
    private Vector3 localPos;


    private Vector3 headLastRot;
    private Vector3 headLastPos;
    private Vector3 headLocalRot;
    private Vector3 headLocalPos;
    private Vector3 headRotVel;
    private Vector3 headXyzVel;

    Transform headTransform;
    GameObject cameraHead;
    GameObject videoControl;


    private void Start()
    {
        cameraHead = GameObject.Find("Camera (head)");
        //RockVR.Video.VideoCaptureCtrl.instance.StartCapture();
        Cursor.visible = false;
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;
        headLastPos = cameraHead.transform.localPosition;
        headLastRot = cameraHead.transform.rotation.eulerAngles;
        strigBuilder.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", "Time", "Click", "Body X,Y,Z",
            "Body Vel X,Y,Z", "Body Angles X,Y,Z", "Body Rot Rate X,Y,Z", "Head Pos wrt. Body X,Y,Z",
            "Head Vel wrt. Body X,Y,Z", "Head Angles wrt. Body X,Y,Z", "Head Rot Rate wrt. Body X,Y,Z");
        strigBuilder.AppendLine();
    }

    private Vector3 calcRotVel()
    {
        Vector3 aux = transform.rotation.eulerAngles;
        Vector3 prevRotVel = rotVel;
        if (true || (lastRot != aux))
        {
            rotVel = aux - lastRot;
            rotVel /= Time.deltaTime;
            lastRot = aux;
        }

        if (Mathf.Abs(rotVel.x) >= 360 || Mathf.Abs(rotVel.y) >= 360 || Mathf.Abs(rotVel.z) >= 360)
        {
            return prevRotVel;
        }

        return rotVel;
    }

    private Vector3 calcXyzVel()
    {
        if (lastPos != transform.position)
        {
            xyzVel = transform.position - lastPos;
            xyzVel /= Time.deltaTime;
            lastPos = transform.position;
        }

        return xyzVel;
    }

    private Vector3 calcHeadRotVel(Transform headTransform)
    {
        Vector3 aux = headTransform.rotation.eulerAngles;
        Vector3 prevHeadRotVel = headRotVel;
        if (true || (headLastRot != aux))
        {
            headRotVel = aux - headLastRot;
            headRotVel /= Time.deltaTime;
            headLastRot = aux;
        }

        if (Mathf.Abs(headRotVel.x) >= 360 || Mathf.Abs(headRotVel.y) >= 360 || Mathf.Abs(headRotVel.z) >= 360)
        {
            return prevHeadRotVel;
        }

        return headRotVel;
    }

    private Vector3 calcHeadXyzVel(Transform headTransform)
    {
        if (headLastPos != headTransform.localPosition)
        {
            headXyzVel = headTransform.localPosition - headLastPos;
            headXyzVel /= Time.deltaTime;
            headLastPos = headTransform.localPosition;
        }
        else
        {
            headXyzVel = headLastPos / Time.deltaTime;
        }

        return headXyzVel;
    }


    void Update()
    {
        localRot = transform.rotation.eulerAngles;
        localPos = transform.localPosition;
        rotVel = calcRotVel();
        xyzVel = calcXyzVel();

        headTransform = cameraHead.transform;
        headLocalPos = headTransform.localPosition;
        headLocalRot = headTransform.localEulerAngles;

        headXyzVel = calcHeadXyzVel(headTransform);
        headRotVel = calcHeadRotVel(headTransform);


        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) &&
            !RequestedToQuit)
        {
            String time = DateTime.Now.ToString("hh.mm.ss.fff");

            strigBuilder.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", time, "1", localPos.ToString("f3"),
                xyzVel.ToString("f3"), localRot.ToString("f3"), rotVel.ToString("f3"), headLocalPos.ToString("f3"),
                headXyzVel.ToString("f3"), headLocalRot.ToString("f3"), headRotVel.ToString("f3"));
            strigBuilder.AppendLine();
            Debug.Log("Mouse Click - Quit in 3s !");
            if (quitOnClick)
                RequestedToQuit = true;
        }

        else
        {
            if (RequestedToQuit)
            {
                if (timeCountedToQuit >= timeToQuit)
                {
//                    RockVR.Video.VideoCaptureCtrl.instance.StopCapture();
                    Application.Quit();
                }
                else
                {
                    timeCountedToQuit = timeCountedToQuit + Time.deltaTime;
                    String time = DateTime.Now.ToString("hh.mm.ss.fff");

                    strigBuilder.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", time, "0",
                        localPos.ToString("f3"), xyzVel.ToString("f3"), localRot.ToString("f3"), rotVel.ToString("f3"),
                        headLocalPos.ToString("f3"), headXyzVel.ToString("f3"), headLocalRot.ToString("f3"),
                        headRotVel.ToString("f3"));
                    strigBuilder.AppendLine();
                }
            }
            else
            {
                String time = DateTime.Now.ToString("hh.mm.ss.fff");
                //Debug.Log(time+ "0" + localPos.ToString() + xyzVel.ToString() + localRot.ToString() + rotVel.ToString());
                strigBuilder.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", time, "0", localPos.ToString("f3"),
                    xyzVel.ToString("f3"), localRot.ToString("f3"), rotVel.ToString("f3"), headLocalPos.ToString("f3"),
                    headXyzVel.ToString("f3"), headLocalRot.ToString("f3"), headRotVel.ToString("f3"));
                //ScreenCapture.CaptureScreenshot("C:\\cyberwell\\Data_collected\\mouse_tracker_report_new_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".png");
                strigBuilder.AppendLine();
            }
        }
    }

    void OnApplicationQuit()
    {
        //String filepath = "C:\\Users\\Neuro\\Downloads";
        System.IO.File.WriteAllText("roation_position_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".csv",
            strigBuilder.ToString());
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}
﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace NetworkAPI
{
    public class NeuLogAPI
    {
       public IEnumerator ClosePreviousExperiment(string uri)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("Error! while starting neulog closing previous experiment uri: " + uwr.error);
            }
            else
            {
                // Successfully send the Http Get Request to the Neulog
                Debug.Log("Received from Neulog Server: " + uwr.downloadHandler.text);
            }
        }

       public IEnumerator StartExperiment(string uri, String freq, String samples)
        {
            uri = uri + ",[" + freq + "]" + ",[" + samples + "]";

            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("Error! while starting neulog start experiment uri: " + uwr.error);
            }
            else
            {
                // Successfully send the Http Get Request to the Neulog
                Debug.Log("Received from Neulog Server: " + uwr.downloadHandler.text);
                // Send Message to python server that we started the experiment
            }
        }

       public IEnumerator StopExperiment(string uri)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("Error! while starting neulog stop experiment uri: " + uwr.error);
            }
            else
            {
                Debug.Log("Received from Neulog Server: " + uwr.downloadHandler.text);
            }
        }
    }
}
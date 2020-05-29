using System;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

namespace NetworkAPI
{
    public class PythonServerAPI : RunAbleThread
    {
        private bool isConnectionDone;
        private RequestSocket client;
        private String HAS_DATA_COLLECTED = "Done";
        private bool _requestCyberSickness = false;
        private bool _waitForServerToRespond = false;

        protected override void Run()
        {
            ForceDotNet.Force();
            isConnectionDone = false;
            string message = null;
            bool gotMessage = false;

            while (Running)
            {
                if (_requestCyberSickness)
                {
                    StartServer();
                    Debug.Log("Sending Request...");
                    SendTCPMessage(_cmd);
                    _requestCyberSickness = false;
                    _waitForServerToRespond = true;
                   
                    if (_waitForServerToRespond)
                    {
                        gotMessage = client.TryReceiveFrameString(out message);
                        if (gotMessage)
                        {
                            Debug.Log("CyberSense Server Successfully Returned Results");
                            isConnectionDone = true;
                            _predictedCyberSickness = float.Parse(message);
                            Debug.Log("Predicted CyberSickness: " + _predictedCyberSickness);
                            _waitForServerToRespond = false;
                            // break;
                            // NetMQConfig.Cleanup();
                        }
                    }
                }
            }
        }

        private void StartServer()
        {
            client = new RequestSocket();
            client.Connect("tcp://localhost:5555");
            Debug.Log("Client Network API is ready");
        }

        public void SendTCPMessage(String msg)
        {
            client?.SendFrame(msg);
        }

        public void SetCmd(String cmd)
        {
            _cmd = cmd;
        }

        public void SetMessageAndSend(String message)
        {
            isConnectionDone = false;
            _requestCyberSickness = true;
            _waitForServerToRespond = true;
            SetCmd(message);
        }

        public bool hasDataProcessed
        {
            get => isConnectionDone;
            set => isConnectionDone = value;
        }
    }
}
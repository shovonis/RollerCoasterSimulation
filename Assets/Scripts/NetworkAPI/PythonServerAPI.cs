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

        protected override void Run()
        {
            ForceDotNet.Force();
            isConnectionDone = false;
            using (client = new RequestSocket())
            {
                client.Connect("tcp://localhost:5555");
                Debug.Log("Initiating Experiment: " + _cmd);
                SendTCPMessage(_cmd);
                string message = null;
                bool gotMessage = false;
                
                while (Running)
                {
                    gotMessage = client.TryReceiveFrameString(out message);
                    if (gotMessage)
                    {
                        Debug.Log("Python Server finished collecting data: " + message);

                        if (message.Contains(HAS_DATA_COLLECTED))
                        {
                            isConnectionDone = true;
                            break;
                        }

                        Debug.LogError("ERROR! Python Server could not collect the sensor data!");
                    }
                }
            }

            NetMQConfig.Cleanup();
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
            SetCmd(message);
        }

        public bool hasDataProcessed
        {
            get => isConnectionDone;
            set => isConnectionDone = value;
        }
    }
}
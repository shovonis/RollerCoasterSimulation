using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Write_Start_Time_Final_Scene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		using (StreamWriter sw = File.AppendText("System_Time_Logged.txt"))
        {
            // Add some text to the file.
            //sw.Write("The Roller Coaster Simulation ");
            sw.WriteLine("Last Scene Start Time:");
            sw.WriteLine("=====================================================");
            // Arbitrary objects can also be written to the file.
            sw.Write("The date and time is: ");
            //sw.WriteLine(DateTime.Now.Hour + ":"+ DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond);
			sw.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HHHH:mm:ss:fff"));//Hour + ":"+ DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond);
			sw.WriteLine();
			sw.WriteLine();
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}


/*

using System;
using System.IO;

class Test
{
    public static void Main()
    {
        // Create an instance of StreamWriter to write text to a file.
        // The using statement also closes the StreamWriter.
        using (StreamWriter sw = new StreamWriter("TestFile.txt"))
        {
            // Add some text to the file.
            sw.Write("This is the ");
            sw.WriteLine("header for the file.");
            sw.WriteLine("-------------------");
            // Arbitrary objects can also be written to the file.
            sw.Write("The date is: ");
            sw.WriteLine(DateTime.Now);
        }
    }
}

    */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public GameObject streamCamera; 
//	void Start()
//	{
//		streamCamera = GameObject.FindGameObjectWithTag("");
//	}
	void Update () {
		var distance = Vector3.Distance(this.transform.position, streamCamera.transform.position);
 
		if(Math.Abs(distance - (-7)) > 0) {
			Debug.Log(distance);
			distance = -7;
			transform.position = (transform.position - streamCamera.transform.position).normalized * distance + streamCamera.transform.position;
		}
	}
}
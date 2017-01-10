using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTime : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		this.GetComponent<Text> ().text = 0.0.ToString("0");
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.GetComponent<Text> ().text = Time.timeSinceLevelLoad.ToString("0");
	}
}

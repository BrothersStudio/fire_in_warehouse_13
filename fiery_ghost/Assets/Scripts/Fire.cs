using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	public float hitpoints;
	public float fireGrowthRate;

	private Light redLight;
	private Light yellowLight; 

	// Use this for initialization
	void Start () 
	{
		redLight = GameObject.Find("Red Light").GetComponent<Light>();
		yellowLight = GameObject.Find("Yellow Light").GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		hitpoints += Time.deltaTime * fireGrowthRate;

		// Red light is twice as wide as yellow
		yellowLight.range = hitpoints / 10;
		redLight.range = hitpoints * 2 / 10;
	}
}

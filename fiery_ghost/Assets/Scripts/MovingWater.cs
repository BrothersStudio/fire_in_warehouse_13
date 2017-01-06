using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWater : MonoBehaviour {

	public float speed;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () 
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();

		rb2d.AddForce (transform.up * speed);
	}
}

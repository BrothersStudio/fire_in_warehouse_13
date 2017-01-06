using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWater : MonoBehaviour {

	public float speed;
	public float waterVariance;
	public float waterLifetime;

	private Rigidbody2D rb2d;
	private float lifetime;

	// Use this for initialization
	void Start () 
	{
		Vector3 playerPosition = GetComponentInParent<Transform> ().position;

		//Vector2 mousePosition2 = Input.mousePosition;
		//Vector3 mousePosition = new Vector3 (mousePosition2 [0], mousePosition2 [1], 0.0f);

		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		float normalizationValue = Vector3.Distance (playerPosition, mousePosition);

		Debug.Log (mousePosition);
		Debug.Log (playerPosition);


		Vector3 fireDirection = new Vector3(
			mousePosition[0] - playerPosition[0], 
			mousePosition[1] - playerPosition[1], 
			0.0f);

		Vector3 offset = new Vector3 (Random.Range (-waterVariance, waterVariance), Random.Range (-waterVariance, waterVariance), 0.0f);

		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.AddForce (((fireDirection + offset) / normalizationValue) * speed);

		lifetime = Time.time;
	}

	void Update()
	{
		if (Time.time > lifetime + waterLifetime)
			Object.Destroy (this.gameObject);
	}
}

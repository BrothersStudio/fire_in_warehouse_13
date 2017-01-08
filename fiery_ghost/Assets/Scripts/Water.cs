using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

	public float speed;
	public float waterVariance;
	public float waterLifetime;
	public MeshRenderer VFX;

	public Material[] waterMaterials;

	private Rigidbody2D rb2d;
	private float lifetime;

	// Use this for initialization
	void Start () 
	{
		VFX.material = waterMaterials[Random.Range(0, waterMaterials.Length)];

		Vector3 playerPosition = GetComponentInParent<Transform> ().position;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		float normalizationValue = Vector3.Distance (playerPosition, mousePosition);

		Vector3 fireDirection = new Vector3(
			mousePosition[0] - playerPosition[0], 
			mousePosition[1] - playerPosition[1], 
			0.0f);

		Vector3 offset = new Vector3 (Random.Range (-waterVariance, waterVariance), Random.Range (-waterVariance, waterVariance), 0.0f);

		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.AddForce (((fireDirection + offset) / normalizationValue) * speed);

		lifetime = Time.timeSinceLevelLoad;
	}

	void Update()
	{
		if (Time.timeSinceLevelLoad > lifetime + waterLifetime)
			Object.Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Fire") 
		{
			float fireHitpoints = other.gameObject.GetComponentInChildren<Fire> ().hitpoints;

			if (fireHitpoints <= 0) 
			{
				Destroy (other.gameObject);
			} 
			else 
			{
				other.gameObject.GetComponentInChildren<Fire>().hitpoints = fireHitpoints - 1.0f;
			}
		}

		Destroy (this.gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

	public float relativeSpeed;

	public GameObject player;

	private float speed;

	void Start () {
		speed = relativeSpeed * player.GetComponent<PlayerController> ().playerSpeed;
	}
		
	void Update () {
		Vector3 playerPosition = player.GetComponent<Transform> ().position;
		Vector3 monsterPosition = this.GetComponent<Transform> ().position;

		float normalizationValue = Vector3.Distance (playerPosition, monsterPosition);

		Vector3 travelDirection = new Vector3(
			playerPosition[0] - monsterPosition[0], 
			playerPosition[1] - monsterPosition[1], 
			0.0f);

		Rigidbody2D rb2d = this.GetComponent<Rigidbody2D> ();

		rb2d.AddForce ((travelDirection / normalizationValue) * speed);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Player") 
		{
			Destroy (this.gameObject);
			GetComponentInParent<MonsterSpawnerController> ().monsterExists = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawnerController : MonoBehaviour {

	public GameObject houseHealthbar;
	public GameObject playerHealthbar;
	public CameraController mainCamera;

	private AudioSource insideFire;

	public GameObject player;
	public GameObject firePrefab;
	public float minDistFromPlayer;
	public float fireCooldown;
	public float multiplierTime;
	public float xSpawn;
	public float ySpawn;

	private float cooldownTime;

	void Start ()
	{
		cooldownTime = fireCooldown;

		insideFire = GetComponent<AudioSource> ();

		GameObject newFire = Instantiate (firePrefab, new Vector3(6f, 6.6f, 0f), Quaternion.identity);

		newFire.GetComponent<Fire> ().houseHealthbar = houseHealthbar;
		newFire.GetComponent<Fire> ().startingHitpoints = 100;

		newFire.GetComponentInChildren<FireHurtbox>().playerHealthbar = playerHealthbar;
		newFire.GetComponentInChildren<FireHurtbox>().mainCamera = mainCamera;

		newFire.GetComponentInChildren<FireSafezone> ().safezoneAudio = insideFire;
	}

	void Update () 
	{
		if (Time.timeSinceLevelLoad >= cooldownTime) 
		{
			for (int i = 0; i <= (Time.timeSinceLevelLoad / multiplierTime); i++) 
			{
				cooldownTime = Time.timeSinceLevelLoad + fireCooldown;

				Vector3 playerLocation = player.GetComponent<Transform> ().position;
				Vector3 spawnLocation = new Vector3 (0.0f, 0.0f, 0.0f); 
				do {
					spawnLocation = new Vector3 (
						Random.Range (-xSpawn, xSpawn), 
						Random.Range (-ySpawn, ySpawn), 
						0.0f);
				} while (Vector3.Distance (playerLocation, spawnLocation) < minDistFromPlayer);

				GameObject newFire = Instantiate (firePrefab, spawnLocation, Quaternion.identity);

				newFire.GetComponent<Fire> ().houseHealthbar = houseHealthbar;

				newFire.GetComponentInChildren<FireHurtbox>().playerHealthbar = playerHealthbar;
				newFire.GetComponentInChildren<FireHurtbox>().mainCamera = mainCamera;

				newFire.GetComponentInChildren<FireSafezone> ().safezoneAudio = insideFire;
			}
		}

	}
}

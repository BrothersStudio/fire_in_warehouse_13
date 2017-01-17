using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawnerController : MonoBehaviour {

	public LayerMask unwalkableMask;

	public DamageController houseHealthbar;
	public DamageController playerHealthbar;
	public UpdateTime score;
	public CameraController mainCamera;

	public GameObject player;
	public GameObject firePrefab;
	public float minDistFromPlayer;
	public float fireCooldown;
	public float multiplierTime;
	public float xSpawn;
	public float ySpawn;

	[HideInInspector]
	public int activeFires = 0;

	private float cooldownTime;

	void Start ()
	{
		cooldownTime = fireCooldown;

		activeFires++;
		GameObject newFire = Instantiate (firePrefab, new Vector3(6f, 6.6f, 0f), Quaternion.identity);

		newFire.GetComponent<Fire> ().houseHealthbar = houseHealthbar;
		newFire.GetComponent<Fire> ().startingHitpoints = 100;
		newFire.GetComponent<Fire> ().score = score;
		newFire.GetComponent<Fire> ().controller = this;

		newFire.GetComponentInChildren<FireHurtbox>().playerHealthbar = playerHealthbar;
		newFire.GetComponentInChildren<FireHurtbox>().mainCamera = mainCamera;
	}

	void Update () 
	{
		if (Time.timeSinceLevelLoad >= cooldownTime) 
		{
			int newFires = 1;
			if (Time.timeSinceLevelLoad / multiplierTime > 3) 
			{
				if (activeFires == 0) 
				{
					newFires = 5;
				} 
				else if (activeFires == 1) 
				{
					newFires = 3;
				} 
				else if (activeFires == 2) 
				{
					newFires = 2;
				} 
				else 
				{
					newFires = 1;
				}
			} 
			else if (Time.timeSinceLevelLoad / multiplierTime > 2) 
			{
				newFires = 3;
			} 
			else if (Time.timeSinceLevelLoad / multiplierTime > 1) 
			{
				newFires = 2;
			} 

			for (int i = 0; i < newFires; i++) 
			{
				cooldownTime = Time.timeSinceLevelLoad + fireCooldown;

				bool walkable = false;
				float playerDist = 0f;
				int checks = 0;
				Vector3 playerLocation = player.GetComponent<Transform> ().position;
				Vector3 spawnLocation = new Vector3 (0.0f, 0.0f, 0.0f); 
				do 
				{
					spawnLocation = new Vector3 (
						Random.Range (-xSpawn, xSpawn), 
						Random.Range (-ySpawn, ySpawn), 
						0.0f);

					checks ++;
					if (checks > 20)
						return;

					walkable = (Physics2D.OverlapCircle ((Vector2)spawnLocation, 1f, unwalkableMask) == null);
					playerDist = Vector3.Distance(spawnLocation, playerLocation);
				}
				while (!walkable || (playerDist < 5f));

				activeFires++;
				GameObject newFire = Instantiate (firePrefab, spawnLocation, Quaternion.identity);

				newFire.GetComponent<Fire> ().houseHealthbar = houseHealthbar;
				newFire.GetComponent<Fire> ().score = score;
				newFire.GetComponent<Fire> ().controller = this;

				newFire.GetComponentInChildren<FireHurtbox>().playerHealthbar = playerHealthbar;
				newFire.GetComponentInChildren<FireHurtbox>().mainCamera = mainCamera;
			}
		}
	}
}

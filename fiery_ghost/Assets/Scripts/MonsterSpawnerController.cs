using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerController : MonoBehaviour {

	public GameObject healthbar;

	public GameObject player;
	public GameObject monsterPrefab;
	public float minDistFromPlayer;
	public float monsterCooldown;
	public float xSpawn;
	public float ySpawn;

	private float cooldownTime;

	[HideInInspector]
	public bool monsterExists;

	void Start () 
	{
		cooldownTime = Time.time + monsterCooldown;
	}

	void Update () 
	{
		if (!monsterExists && Time.time >= cooldownTime) 
		{
			Debug.Log("Spawned in monster");

			monsterExists = true;
			cooldownTime = Time.time + monsterCooldown;


			Vector3 playerLocation = player.GetComponent<Transform> ().position;
			Vector3 spawnLocation = new Vector3(0.0f,0.0f,0.0f); 
			do 
			{
			spawnLocation = new Vector3 (
				               Random.Range (-xSpawn, xSpawn), 
				               Random.Range (-ySpawn, ySpawn), 
				               0.0f);
			} while (Vector3.Distance (playerLocation, spawnLocation) < minDistFromPlayer);

			GameObject newMonster = Instantiate(monsterPrefab, spawnLocation, player.GetComponent<Transform> ().rotation, this.transform);
			newMonster.GetComponent<MonsterController> ().player = player;
			newMonster.GetComponent<MonsterController> ().healthbar = healthbar;
		}

	}
}

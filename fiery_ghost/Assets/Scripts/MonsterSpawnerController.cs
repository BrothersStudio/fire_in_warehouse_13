using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerController : MonoBehaviour {

	public GameObject player;
	public GameObject monsterPrefab;
	public float monsterCooldown;
	public float xSpawn;
	public float ySpawn;

	private float cooldownTime;
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

			Vector3 spawnLocation = new Vector3 (
				                       Random.Range (-xSpawn, xSpawn), 
				                       Random.Range (-ySpawn, ySpawn), 
				                       0.0f);

			GameObject newMonster = Instantiate(monsterPrefab, spawnLocation, player.GetComponent<Transform> ().rotation, this.transform);
			newMonster.GetComponent<MonsterController> ().player = player;
		}

	}
}

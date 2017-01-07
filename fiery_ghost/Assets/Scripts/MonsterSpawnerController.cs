using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerController : MonoBehaviour {

	public GameObject player;
	public GameObject monsterPrefab;
	public float xSpawn;
	public float ySpawn;

	void Start () {
		
		Vector3 spawnLocation = new Vector3 (
			Random.Range (-xSpawn, xSpawn), 
			Random.Range (-ySpawn, ySpawn), 
			0.0f);

		GameObject newMonster = Instantiate (monsterPrefab, spawnLocation, player.GetComponent<Transform>().rotation);
		newMonster.GetComponent<MonsterController> ().player = player;
	}

	void Update () {
		
	}
}

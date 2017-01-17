using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerController : MonoBehaviour {

	public DamageController playerHealthbar;
	public CameraController mainCamera;

	public GameObject player;
	public GameObject monsterPrefab;
	public float minDistFromPlayer;
	public float monsterCooldown;
	public float xSpawn;
	public float ySpawn;

	public AudioClip heartBeatFast;
	private AudioSource heartbeatAudio;

	private float monsterNumber = 0f;
	private float cooldownTime = 0f;

	[HideInInspector]
	public bool monsterExists;
	private GameObject newMonster;
	private bool timeSet;

	void Start () 
	{
		cooldownTime = 7f;

		monsterExists = false;
		timeSet = true;

		heartbeatAudio = GetComponent<AudioSource> ();
	}

	void Update () 
	{
		if (!monsterExists && !timeSet) 
		{
			//heartbeatAudio.Stop ();

			cooldownTime = Time.timeSinceLevelLoad + monsterCooldown;
			timeSet = true;
		}

		if (!monsterExists && Time.timeSinceLevelLoad >= cooldownTime) 
		{
			monsterExists = true;
			timeSet = false;

			Vector3 playerLocation = player.GetComponent<Transform> ().position;
			Vector3 spawnLocation = new Vector3(0.0f,0.0f,0.0f);  // Need to instantiate before the loop

			do 
			{
			spawnLocation = new Vector3 (
				               Random.Range (-xSpawn, xSpawn), 
				               Random.Range (-ySpawn, ySpawn), 
				               0.0f);
			} 
			while (Vector3.Distance (playerLocation, spawnLocation) < minDistFromPlayer);

			newMonster = Instantiate(monsterPrefab, spawnLocation, player.GetComponent<Transform> ().rotation, this.transform);

			newMonster.GetComponent<MonsterController> ().player = player;
			newMonster.GetComponent<MonsterController> ().playerHealthbar = playerHealthbar;
			newMonster.GetComponent<MonsterController> ().mainCamera = mainCamera;

			newMonster.GetComponent<MonsterController> ().speed = Mathf.Clamp (newMonster.GetComponent<MonsterController> ().speed + monsterNumber * 0.5f, 6f, 7f);
			newMonster.GetComponent<MonsterController> ().lifetime += monsterNumber * 20;
			monsterNumber++;
		}

		// Choosing heartbeat clip
		if (monsterExists && !heartbeatAudio.isPlaying) 
		{
			Vector3 playerLocation = player.GetComponent<Transform> ().position;
			Vector3 monsterLocation = newMonster.GetComponent<Transform> ().position;

			if (Vector3.Distance (playerLocation, monsterLocation) <= 10f) 
			{
				heartbeatAudio.clip = heartBeatFast;
				heartbeatAudio.Play ();
			} 
		}
	}
}

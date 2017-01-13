using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerController : MonoBehaviour {

	public GameObject playerHealthbar;
	public CameraController mainCamera;

	public GameObject player;
	public GameObject monsterPrefab;
	public float minDistFromPlayer;
	public float monsterCooldown;
	public float xSpawn;
	public float ySpawn;

	//public AudioClip heartbeat_slow;
	public AudioClip heartBeatSlow;
	public AudioClip heartBeatFast;
	private AudioSource heartbeatAudio;

	private float cooldownTime = 0f;

	[HideInInspector]
	public bool monsterExists;
	private GameObject newMonster;
	private bool timeSet;

	void Start () 
	{
		monsterExists = false;
		timeSet = false;

		heartbeatAudio = GetComponent<AudioSource> ();
	}

	void Update () 
	{
		// Heartbeat Controller
		if (monsterExists) 
		{
			//heartbeatAudio.PlayOneShot(heartBeatSlow, 1f);
		}

		if (!monsterExists && !timeSet) 
		{
			cooldownTime = Time.timeSinceLevelLoad + monsterCooldown;
			timeSet = true;
		}

		if (!monsterExists && Time.timeSinceLevelLoad >= cooldownTime) 
		{
			monsterExists = true;
			timeSet = false;

			Vector3 playerLocation = player.GetComponent<Transform> ().position;
			Vector3 spawnLocation = new Vector3(0.0f,0.0f,0.0f); 

			do 
			{
			spawnLocation = new Vector3 (
				               Random.Range (-xSpawn, xSpawn), 
				               Random.Range (-ySpawn, ySpawn), 
				               0.0f);
			} while (Vector3.Distance (playerLocation, spawnLocation) < minDistFromPlayer);

			newMonster = Instantiate(monsterPrefab, spawnLocation, player.GetComponent<Transform> ().rotation, this.transform);

			newMonster.GetComponent<MonsterController> ().player = player;
			newMonster.GetComponent<MonsterController> ().playerHealthbar = playerHealthbar;
			newMonster.GetComponent<MonsterController> ().mainCamera = mainCamera;
		}
	}
}

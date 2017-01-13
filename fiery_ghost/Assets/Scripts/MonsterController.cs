using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour {

	public GameObject player;
	public GameObject playerHealthbar;
	public CameraController mainCamera;

	private Vector3[] corners = new [] { 
		new Vector3 (20f, 20f, 0f),
		new Vector3 (20f, -20f, 0f),
		new Vector3 (-20f, 20f, 0f),
		new Vector3 (-20f, -20f, 0f)};

	public float damage;
	public float speed;

	public float playerChaseTime;
	private float playerChaseCooldown;
	private bool playerChase;
	private int cornerInd = 0;

    int targetIndex;
    
    Vector2[] path;

	void Start () 
    {
		playerChase = true;

		playerChaseCooldown = Time.timeSinceLevelLoad + playerChaseTime;

        StartCoroutine (RefreshPath ());
	}

	Vector3 GetCurrentDestination()
	{
		if (Time.timeSinceLevelLoad >= playerChaseCooldown && playerChase) 
		{
			playerChase = false;

			playerChaseCooldown = Time.timeSinceLevelLoad + playerChaseTime;

			cornerInd = Random.Range (0, 4);
		} 
		else if (Time.timeSinceLevelLoad >= playerChaseCooldown && !playerChase) 
		{
			playerChase = true;

			playerChaseCooldown = Time.timeSinceLevelLoad + playerChaseTime;
		}


		if (playerChase) 
		{
			return player.GetComponent<Transform> ().position;
		} 
		else 
		{
			return corners [cornerInd];
		}
	}
    
	/*void Update()
	{
		if (Vector3.Distance (transform.position, player.GetComponent<Transform> ().position) <= 5f) 
		{
			playerChase = true;
		}
	}*/

	IEnumerator RefreshPath() 
    {
		Vector2 targetPositionOld = (Vector2)GetCurrentDestination() + Vector2.up; // ensure != to target.position initially
			
		while (true) {
			if (targetPositionOld != (Vector2)GetCurrentDestination()) 
            {
				targetPositionOld = (Vector2)GetCurrentDestination();

				path = Pathfinding.RequestPath (transform.position, GetCurrentDestination());
				StopCoroutine ("FollowPath");
				StartCoroutine ("FollowPath");
			}

			yield return new WaitForSeconds (.25f);
		}
	}    
    
	IEnumerator FollowPath() 
    {
		if (path.Length > 0) 
        {
			targetIndex = 0;
			Vector2 currentWaypoint = path [0];

			while (true) 
            {
				if ((Vector2)transform.position == currentWaypoint) 
                {
					targetIndex++;
					if (targetIndex >= path.Length) {
						yield break;
					}
					currentWaypoint = path [targetIndex];
				}
					
				transform.position = Vector2.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
				yield return null;

			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Player") 
		{
			Destroy (this.gameObject);
			GetComponentInParent<MonsterSpawnerController> ().monsterExists = false;

			mainCamera.shakeDuration = 1f;
			mainCamera.shakeAmount = 1f;
			mainCamera.shakeDecreaseFactor = 2f;

			playerHealthbar.GetComponent<Image> ().fillAmount = playerHealthbar.GetComponent<Image> ().fillAmount - damage;
		}
	}
}

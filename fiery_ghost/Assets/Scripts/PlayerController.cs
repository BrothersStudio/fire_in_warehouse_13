using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float playerSpeed;
	public float fireSlowdownFactor;
	public float waterVariance;

	public GameObject water;
	public Transform waterSpawn;
	public float fireRate;

	public AudioClip hurt;
	public AudioClip evilLaugh;
	public AudioClip[] footsteps;
	public AudioSource playerSource;
	public AudioSource waterSource;

	private bool slowed = false;
	private float nextFire;
	private Rigidbody2D rb2d;
	private Camera cam;

	void Start()
	{
		playerSource = GetComponent<AudioSource> ();

		rb2d = GetComponent<Rigidbody2D> ();

		// Performance consideration
		cam = Camera.main;
	}

	void Update()
	{
		// Rotate player to face mouse pointer
		Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

		if (Input.GetMouseButton (0) && Time.timeSinceLevelLoad > nextFire) 
		{
			nextFire = Time.timeSinceLevelLoad + fireRate;

			Vector3 offset = new Vector3 (
				Random.Range (-waterVariance, waterVariance), 
				Random.Range (-waterVariance, waterVariance), 
				0.0f);

			Quaternion randomSpin = waterSpawn.rotation;
			randomSpin.z = randomSpin.z + Random.Range (0.0f, 1.0f);

			Instantiate (water, waterSpawn.position + offset, randomSpin);
            if (!slowed) 
			{
				slowed = true;
				playerSpeed = playerSpeed / fireSlowdownFactor;
			}

			if (!waterSource.isPlaying) 
			{
				waterSource.pitch = Random.Range (0.5f, 1f);
				waterSource.Play ();
			}
		} 
		else if (slowed == true && Time.timeSinceLevelLoad > nextFire) 
		{
			slowed = false;
			playerSpeed = playerSpeed * fireSlowdownFactor;
			waterSource.Stop ();
		}
	}
		
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		if (!playerSource.isPlaying && (moveHorizontal != 0 || moveVertical != 0)) 
		{
			playerSource.clip = footsteps [Random.Range (0, footsteps.Length)];
			playerSource.Play ();
		}

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		movement.Normalize ();
		movement = movement * playerSpeed;

		rb2d.AddForce (movement);
	}

	public void Damage(string source)
	{
		if (source == "Fire") 
		{
			playerSource.PlayOneShot(hurt, 0.2f);
		} 
		else if (source == "Monster") 
		{
			playerSource.PlayOneShot(evilLaugh, 0.4f);
		}
	}
}

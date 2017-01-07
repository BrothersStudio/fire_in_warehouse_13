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

	private bool slowed = false;
	private float nextFire;
	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update()
	{
		// Rotate player to face mouse pointer
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

		if (Input.GetMouseButton (0) && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;

			Vector3 offset = new Vector3 (Random.Range (-waterVariance, waterVariance), Random.Range (-waterVariance, waterVariance), 0.0f);

			Instantiate (water, waterSpawn.position + offset, waterSpawn.rotation);

			if (!slowed) {
				slowed = true;
				playerSpeed = playerSpeed / fireSlowdownFactor;
			}
		} 
		else if (slowed == true && Time.time > nextFire) 
		{
			slowed = false;
			playerSpeed = playerSpeed * fireSlowdownFactor;
		}
	}
		
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		rb2d.AddForce (movement * playerSpeed);
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("PickUp")) 
		{

		}

	}
		
}

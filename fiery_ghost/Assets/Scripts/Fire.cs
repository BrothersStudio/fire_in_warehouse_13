using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour {

	[HideInInspector]
	public DamageController houseHealthbar;
	[HideInInspector]
	public FireSpawnerController controller;

	private AudioSource fireSource;

	public float startingHitpoints;
	public float nominalHitpoints;
	public float maxHitpoints;
	public float fireGrowthRate;

	public float houseDamage;
	public float houseDamageRate;
	public float houseDamageAge;

	public float flickerRate;
	public Light redLight;
	public Light yellowLight; 
	public GameObject sprite;
	public Texture2D icon;
	private float startingIconSize;
	public float iconSize;

	[HideInInspector]
	public GUIStyle gooey;
	[HideInInspector]
	public float hitpoints;
	[HideInInspector]
	public UpdateTime score;

	private float timeOfBirth;
	private float nextFlicker;
	private float nextHouseDamage;

	private Vector3 startingSpriteSize;
	private float startingMonsterExclusionRadius;
	private float startingHitboxRadius;

	private Camera cam;
	private bool visible;
	private Vector2 indRange;
	float scaleRes = Screen.width / 500;

	void Start () 
	{
		timeOfBirth = Time.timeSinceLevelLoad;

		hitpoints = Random.Range(startingHitpoints, nominalHitpoints);

		fireSource = GetComponent<AudioSource> ();

		startingSpriteSize = sprite.GetComponent<Transform>().localScale;
		startingMonsterExclusionRadius = GetComponentsInChildren<CircleCollider2D> () [0].radius;
		startingHitboxRadius = GetComponentsInChildren<CircleCollider2D> () [1].radius;
		startingIconSize = iconSize;

		visible = sprite.GetComponent<SpriteRenderer> ().isVisible;
		cam = Camera.main;

		nextHouseDamage = houseDamageRate;
		nextFlicker = 0f;

		indRange.x = Screen.width - (Screen.width / 6);
		indRange.y = Screen.height - (Screen.height / 7);
		indRange /= 1.7f;

		gooey.normal.textColor = new Vector4 (0, 0, 0, 0);
	}

	void Update () 
	{
		if (hitpoints < maxHitpoints) 
		{
			hitpoints += Time.deltaTime * fireGrowthRate;

			// Red light is twice as wide as yellow
			yellowLight.range = hitpoints / 10;
			redLight.range = hitpoints * 2 / 10;

			sprite.GetComponent<Transform> ().localScale = startingSpriteSize * (hitpoints / nominalHitpoints);

			GetComponentsInChildren<CircleCollider2D> () [0].radius = startingMonsterExclusionRadius * (hitpoints / nominalHitpoints);
			GetComponentsInChildren<CircleCollider2D> () [1].radius = startingHitboxRadius * (hitpoints / nominalHitpoints);

			iconSize = startingIconSize * (hitpoints / nominalHitpoints);

			fireSource.volume = (hitpoints / nominalHitpoints);
		}

		if (Time.timeSinceLevelLoad >= timeOfBirth + houseDamageAge) 
		{
			if (Time.timeSinceLevelLoad > nextHouseDamage) 
			{
				nextHouseDamage = Time.timeSinceLevelLoad + houseDamageRate;

				houseHealthbar.Damage(houseDamage * (hitpoints / nominalHitpoints), "House");
			}
		}

		if (!fireSource.isPlaying) 
		{
			fireSource.Play ();
		}
	}

	void LateUpdate()
	{
		if (Time.timeSinceLevelLoad > nextFlicker) 
		{
			nextFlicker = Time.timeSinceLevelLoad + Random.Range (0f, flickerRate);

			yellowLight.range = Mathf.Clamp(yellowLight.range + Random.Range (-0.5f, 0.5f), 1.5f, 3.0f);

			yellowLight.intensity = yellowLight.intensity + Random.Range (-0.1f, 0.1f);
			redLight.intensity = redLight.intensity + Random.Range (-0.1f, 0.2f);
		}
	}

	public void ReportDeath ()
	{
		controller.activeFires--;
	}

	void OnGUI() 
	{
		if (!visible) 
		{
			Vector3 dir = transform.position - cam.transform.position;
			dir = Vector3.Normalize (dir);
			dir.y *= -1f;

			Vector2 indPos = new Vector2 (indRange.x * dir.x, indRange.y * dir.y);
			indPos = new Vector2 ((Screen.width / 2) + indPos.x,
				(Screen.height / 2) + indPos.y);

			Vector3 pdir = transform.position - cam.ScreenToWorldPoint (new Vector3 (indPos.x, indPos.y, 
				               transform.position.z));

			pdir = Vector3.Normalize (pdir);

			float angle = Mathf.Atan2 (pdir.x, pdir.y) * Mathf.Rad2Deg;

			GUIUtility.RotateAroundPivot (angle, indPos);
			GUI.Box (new Rect (indPos.x, indPos.y, scaleRes * iconSize, scaleRes * iconSize), icon, gooey);
			GUIUtility.RotateAroundPivot (0, indPos);
		}
	}

	void OnBecameInvisible() 
	{
		visible = false;
	}

	void OnBecameVisible() 
	{
		visible = true;
	}
}

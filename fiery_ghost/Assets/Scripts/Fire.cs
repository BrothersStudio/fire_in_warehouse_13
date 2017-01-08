using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour {

	public GameObject healthbar;

	public float startingHitpoints;
	public float maxHitpoints;
	public float fireGrowthRate;

	public float damage;
	public float damageRate;

	public Light redLight;
	public Light yellowLight; 
	public GameObject sprite;
	public Texture2D icon;
	public float iconSize;

	[HideInInspector]
	public GUIStyle gooey;
	public float hitpoints;

	private float nextDamage;
	private Vector3 startingSpriteSize;
	private Camera cam;
	private bool visible;
	private Vector2 indRange;
	float scaleRes = Screen.width / 500;

	void Start () 
	{
		hitpoints = startingHitpoints;

		startingSpriteSize = sprite.GetComponent<Transform>().localScale;
		visible = sprite.GetComponent<SpriteRenderer> ().isVisible;
		cam = Camera.main;

		nextDamage = damageRate;

		indRange.x = Screen.width - (Screen.width / 6);
		indRange.y = Screen.height - (Screen.height / 7);
		indRange /= 2f;

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

			sprite.GetComponent<Transform> ().localScale = startingSpriteSize * (hitpoints / startingHitpoints);
		}

		if (Time.time > nextDamage) 
		{
			nextDamage = Time.time + damageRate;

			healthbar.GetComponent<Image> ().fillAmount = healthbar.GetComponent<Image> ().fillAmount - damage * (hitpoints / startingHitpoints);
		}
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour {

	public GameObject healthbar;
	public GameObject fireArrowPrefab;

	public float maxHitpoints;
	public float fireGrowthRate;

	public float damage;
	public float damageRate;

	[HideInInspector]
	public float hitpoints;

	public Light redLight;
	public Light yellowLight; 
	public Transform sprite;

	private float nextDamage;
	private Vector3 startingSpriteSize;
	private GameObject myArrow;

	void Start () 
	{
		hitpoints = maxHitpoints;

		startingSpriteSize = sprite.localScale;

		nextDamage = damageRate;

	}

	void Update () 
	{
		hitpoints += Time.deltaTime * fireGrowthRate;

		// Red light is twice as wide as yellow
		yellowLight.range = hitpoints / 10;
		redLight.range = hitpoints * 2 / 10;

		sprite.localScale = startingSpriteSize * (hitpoints / maxHitpoints);

		if (Time.time > nextDamage) 
		{
			nextDamage = Time.time + damageRate;

			healthbar.GetComponent<Image> ().fillAmount = healthbar.GetComponent<Image> ().fillAmount - damage * (hitpoints / maxHitpoints);
		}
	}

	void OnBecameInvisible() 
	{
		Debug.Log ("Can't see");
	}

	void OnBecameVisible() 
	{
		Debug.Log ("Can see");
	}
}

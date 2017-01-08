using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour {

	public GameObject healthbar;

	public float maxHitpoints;
	public float fireGrowthRate;

	public float damage;
	public float damageRate;

	public float hitpoints;
	public Light redLight;
	public Light yellowLight; 
	public Transform sprite;

	private float nextDamage;
	private Vector3 startingSpriteSize;

	void Start () 
	{
		hitpoints = maxHitpoints;

		startingSpriteSize = sprite.localScale;

		nextDamage = damageRate;
	}
	
	// Update is called once per frame
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

		if (this.GetComponentInChildren<Renderer>().isVisible)
		{
			Debug.Log ("I see fire!");
		}
	}
}

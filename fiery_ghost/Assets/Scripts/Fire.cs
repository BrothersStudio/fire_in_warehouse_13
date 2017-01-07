using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	public float maxHitpoints;
	public float fireGrowthRate;

	public float hitpoints;
	public Light redLight;
	public Light yellowLight; 
	public Transform sprite;

	private Vector3 startingSpriteSize;

	void Start () 
	{
		hitpoints = maxHitpoints;

		startingSpriteSize = sprite.localScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		hitpoints += Time.deltaTime * fireGrowthRate;

		// Red light is twice as wide as yellow
		yellowLight.range = hitpoints / 10;
		redLight.range = hitpoints * 2 / 10;

		sprite.localScale = startingSpriteSize * (hitpoints / maxHitpoints);
	}
}

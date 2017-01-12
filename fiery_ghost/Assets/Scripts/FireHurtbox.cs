using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireHurtbox : MonoBehaviour {

	public Fire fire;

	public GameObject playerHealthbar;

	public float playerDamage;
	public float playerDamageRate;

	private float nextPlayerDamage;

	void Start()
	{
		nextPlayerDamage = 0f;
	}

	void OnTriggerStay2D(Collider2D other) 
	{
		if (other.tag == "Player" && Time.timeSinceLevelLoad > nextPlayerDamage) 
		{
			nextPlayerDamage = Time.timeSinceLevelLoad + playerDamageRate;

			playerHealthbar.GetComponent<Image> ().fillAmount = playerHealthbar.GetComponent<Image> ().fillAmount - playerDamage * (fire.hitpoints / fire.nominalHitpoints);
		}
	}
}

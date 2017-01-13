using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireHurtbox : MonoBehaviour {

	public Fire fire;

	public GameObject playerHealthbar;
	public CameraController mainCamera;

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

			mainCamera.shakeDuration = 0.5f;
			mainCamera.shakeAmount = 1f;
			mainCamera.shakeDecreaseFactor = 5f;

			other.GetComponent<PlayerController> ().Damage ("Fire");
			playerHealthbar.GetComponent<Image> ().fillAmount = playerHealthbar.GetComponent<Image> ().fillAmount - playerDamage * (fire.hitpoints / fire.nominalHitpoints);
		}
	}
}

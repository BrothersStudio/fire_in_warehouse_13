using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageController : MonoBehaviour {

	public Image mainBar;
	public Image lagBar;

	private float nextUpdate = 0f;
	private float batchDamage = 0f;

	void LateUpdate()
	{
		if (Time.timeSinceLevelLoad > nextUpdate) 
		{
			nextUpdate = Time.timeSinceLevelLoad + 5.0f;

			if (batchDamage > 0f) 
			{
				ApplyDamage ();
			}
		}
	}

	public void Damage(float damage, string target)
	{
		batchDamage += damage;
		if (target == "Player")
			ApplyDamage ();
	}

	void ApplyDamage ()
	{
		mainBar.fillAmount -= batchDamage;
		StartCoroutine (LagDamage (batchDamage));
		batchDamage = 0f;
	}

	IEnumerator LagDamage(float damage) 
	{
		yield return new WaitForSeconds(1);
		lagBar.fillAmount -= damage;
	}
}


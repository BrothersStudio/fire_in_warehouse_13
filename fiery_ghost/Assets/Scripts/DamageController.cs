using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageController : MonoBehaviour {

	public Image mainBar;
	public Image lagBar;

	public void Damage(float damage)
	{
		mainBar.fillAmount -= damage;
		StartCoroutine (LagDamage (damage));
	}

	IEnumerator LagDamage(float damage) 
	{
		yield return new WaitForSeconds(1);
		lagBar.fillAmount -= damage;
	}
}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	public GameObject player;

	public GameObject monsterController;
	public GameObject fireController;

	public GameObject houseHealthbar;
	public GameObject playerHealthbar;

	public GameObject scoreText;

	public GameObject gameOverTitle;
	public GameObject gameOverScore;
	public GameObject gameOverButton;

	void LateUpdate () 
	{
		float houseHealth = houseHealthbar.GetComponent<Image> ().fillAmount;
		float playerHealth = playerHealthbar.GetComponent<Image> ().fillAmount;

		if (houseHealth <= 0 || playerHealth <= 0) 
		{
			// Game over!
			string score = scoreText.GetComponent<Text>().text;
			gameOverScore.GetComponent<Text> ().text = "Score: " + score;

			player.SetActive (false);

			gameObject.SetActive(false);  // This is the game over monitor
			monsterController.SetActive (false);
			fireController.SetActive (false);

			scoreText.SetActive (false);

			gameOverTitle.SetActive (true);
			gameOverScore.SetActive (true);
			gameOverButton.SetActive (true);
		}
	}
}

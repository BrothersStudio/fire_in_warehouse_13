using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	public GameObject player;
	public GameObject mainCamera;

	public GameObject monsterController;
	public GameObject fireController;

	public GameObject houseHealthbar;
	public GameObject playerHealthbar;

	public GameObject scoreText;

	public GameObject gameOverTitle;
	public GameObject gameOverScore;
	public GameObject gameOverContinue;
	public GameObject gameOverQuit;

	private bool isGameOver = false;
	private AudioSource gameOverSource;
	private Camera mainCam;

	void Start ()
	{
		gameOverSource = GetComponent<AudioSource> ();	
		mainCam = Camera.main;
	}

	void LateUpdate () 
	{
		float houseHealth = houseHealthbar.GetComponent<Image> ().fillAmount;
		float playerHealth = playerHealthbar.GetComponent<Image> ().fillAmount;

		if ((houseHealth <= 0 || playerHealth <= 0) && !isGameOver) 
		{
			// Game over!
			isGameOver = true;

			Debug.Log("Game lasted: " + Time.timeSinceLevelLoad.ToString());

			string score = scoreText.GetComponent<Text>().text;
			gameOverScore.GetComponent<Text> ().text = "Score: " + score;

			player.SetActive (false);
			mainCamera.GetComponent<AudioListener> ().enabled = true;

			monsterController.SetActive (false);
			fireController.SetActive (false);

			scoreText.SetActive (false);

			gameOverTitle.SetActive (true);
			gameOverScore.SetActive (true);
			gameOverContinue.SetActive (true);
			gameOverQuit.SetActive (true);

			AudioSource[] cameraSources = mainCam.GetComponentsInChildren<AudioSource> ();
			for (int i = 0; i < cameraSources.Length; i++) 
			{
				cameraSources [i].Stop ();
			}
			gameOverSource.Play ();
		}
	}
}

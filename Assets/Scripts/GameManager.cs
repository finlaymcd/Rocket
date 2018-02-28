using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject MenuUI;
	public GameObject GameUI;
	public AsteroidSpawner[] Spawners;
	public RocketShip Rocket;
	private Vector3 RocketStart;
	public ScoreCounter scoreCounter;
	public GameObject ExplosionParticle;
	private GameObject SpawnedExplosion;
	public Text HighScoreText;

	private int CurrentHighScore = 0;

	// Use this for initialization
	void Start () {
		GameUI.SetActive (false);
		RocketStart = Rocket.gameObject.transform.position;
		CurrentHighScore = PlayerPrefs.GetInt ("High Score");
		if(CurrentHighScore != 0){
			HighScoreText.text = "High Score: " + CurrentHighScore;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void BeginGame(){
		MenuUI.SetActive (false);
		GameUI.SetActive (true);
		foreach (AsteroidSpawner a in Spawners) {
			a.BeginSpawning ();
		}
		scoreCounter.BeginCounting ();
	}

	public void EndGame(){
		foreach (AsteroidSpawner a in Spawners) {
			a.EndSpawning ();
			a.ResetSpawner ();
		}
		Quaternion ExplosionRot = Quaternion.Euler (new Vector3(-90.0f, 0.0f, 0.0f));
		SpawnedExplosion = Instantiate (ExplosionParticle, Rocket.transform.position, ExplosionRot);
		scoreCounter.StopCounting ();
		Vector3 FlyInPos = new Vector3 (RocketStart.x, RocketStart.y - 3.0f, RocketStart.z);
		Rocket.gameObject.transform.position = FlyInPos;
		StartCoroutine ("ResetGame");

	}

	IEnumerator ResetGame(){
		yield return new WaitForSeconds (4.0f);
		iTween.MoveTo (Rocket.gameObject, RocketStart, 2.0f);
		MenuUI.SetActive (true);
		GameUI.SetActive (false);
		Rocket.gameObject.SetActive (true);
		if(SpawnedExplosion != null){
			Destroy (SpawnedExplosion);
		}
		if(scoreCounter.GetScore() > CurrentHighScore){
			CurrentHighScore = scoreCounter.GetScore ();
			HighScoreText.text = "High Score: " + CurrentHighScore;
		}
		scoreCounter.ResetScore ();
		PlayerPrefs.SetInt ("High Score", CurrentHighScore);
	}
}

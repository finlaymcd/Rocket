using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

	public GameObject Asteroid;
	public float StartingAsteroidinterval = 3.0f;
	public float SpawnWidth = 1.0f;
	public float SpawnIncreaseRate = 0.01f;

	private bool CanSpawn = false;
	private float CurrentAsteroidInterval = 10.0f;
	private float CurrentInterval = 0.0f;


	// Use this for initialization
	void Start () {
		CurrentAsteroidInterval = StartingAsteroidinterval;
	}
	
	// Update is called once per frame
	void Update () {
		if(CanSpawn){
			CurrentInterval -= Time.deltaTime;
			if(CurrentInterval < 0.0f){
				SpawnAsteroid ();
				CurrentInterval = CurrentAsteroidInterval;
			}
		}
	}

	void SpawnAsteroid(){
		float rand = Random.Range (SpawnWidth * -1, SpawnWidth);
		Vector3 SpawnPos = new Vector3 (transform.position.x + rand, transform.position.y, transform.position.z);
		Instantiate (Asteroid, SpawnPos, transform.rotation);
		if(CurrentAsteroidInterval > 0.4f){
			CurrentAsteroidInterval -= SpawnIncreaseRate;
		}
	}

	public void BeginSpawning(){
		CanSpawn = true;
	}

	public void EndSpawning(){
		CanSpawn = false;
	}

	public void ResetSpawner(){
		CurrentAsteroidInterval = StartingAsteroidinterval;
	}
}

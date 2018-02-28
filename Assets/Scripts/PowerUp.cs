using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	public float TimeLimit = 0.0f; //0 means no timer
	bool PowerUpActive = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (PowerUpActive) {
			TimeLimit -= Time.deltaTime;
			if (TimeLimit < 0.0f) {
				EndPowerUp ();
			}
		} else {
			Vector3 newPos = new Vector3 (transform.position.x, transform.position.y - (3 * Time.deltaTime), transform.position.z);
			transform.position = newPos;
		}
	}

	public virtual void ActivatePowerUp(RocketShip ship){
		if(gameObject.GetComponent<SpriteRenderer>() != null){
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	void EndPowerUp(){

	}
}

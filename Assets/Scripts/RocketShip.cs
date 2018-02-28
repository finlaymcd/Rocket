using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour {


	private float CurrentImpulseStrength = 0.0f; // Tracks the force of the movement
	private float CurrentImpulseDirection = 1.0f; //either 1 or -1, effects direction
	public float MaxImpulseStrength = 1.0f;
	public float LinearDrag = 1.0f;
	public float MaxRotation = 20.0f;
	public GameManager gameManager;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentImpulseStrength > 0.0f) {
			UpdateImpulse ();
		} else {
			CurrentImpulseStrength = 0.0f;
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			ImpulseLeft ();
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			ImpulseRight ();
		}
		if(Input.touchCount > 0){
			Vector2 TouchLocation = Input.GetTouch (0).position;
			Vector3 WorldPos = Camera.main.ScreenToWorldPoint (new Vector3(TouchLocation.x, TouchLocation.y, 0.0f));
			if (WorldPos.x > 0) {
				ImpulseRight ();
			} else {
				ImpulseLeft ();
			}
		}
		Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
		pos.x = Mathf.Clamp01(pos.x);
		pos.y = Mathf.Clamp01(pos.y);
		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}

	// begin move left
	void ImpulseLeft(){
		CurrentImpulseStrength = MaxImpulseStrength;
		CurrentImpulseDirection = -1.0f;
	}

	//begin move right
	void ImpulseRight(){
		CurrentImpulseStrength = MaxImpulseStrength;
		CurrentImpulseDirection = 1.0f;
	}

	void UpdateImpulse(){
		Vector3 newLocation = new Vector3 (gameObject.transform.position.x + (CurrentImpulseStrength * CurrentImpulseDirection), gameObject.transform.position.y, gameObject.transform.position.z);
		float strength = Mathf.Abs (CurrentImpulseStrength);
		float rotation = Mathf.Lerp (0.0f, MaxRotation, CurrentImpulseStrength);
		if(CurrentImpulseDirection > 0.0f){
			rotation *= -1;
		}
		gameObject.transform.position = newLocation;
		Vector3 rotEuler = new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.eulerAngles.y, rotation);
		Quaternion newRotation = Quaternion.Euler(rotEuler);
		gameObject.transform.rotation = newRotation;
		CurrentImpulseStrength -= (Time.deltaTime * LinearDrag); 
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Asteroid"){
			DestroyShip ();
		}
		else if(other.gameObject.tag == "PowerUp"){
			if(other.GetComponent<PowerUp>() != null){
				other.GetComponent<PowerUp> ().ActivatePowerUp (this);
			}
		}

	}

	void DestroyShip(){
		gameObject.SetActive (false);
		gameManager.EndGame ();
	}
		
}

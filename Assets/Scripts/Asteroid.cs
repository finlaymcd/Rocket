using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	Vector3 MovementVector;
	float Spin;
	Vector3 SizeVector;
	public float MaxXSpeed = 0.5f;
	public float MinXSpeed = 0.0f;
	public float MaxYSpeed = 1.0f;
	public float MinYSpeed = 0.5f;
	public float MinScale = 0.5f;
	public float MaxScale = 1.5f;
	public float Lifetime = 3.0f;

	private float SpinFactor = 1.0f;

	// Use this for initialization
	void Start () {
		float x = Random.Range (MinXSpeed, MaxXSpeed);
		float y = Random.Range (MinYSpeed, MaxYSpeed);
		y *= -1;
		MovementVector = new Vector3 (x, y, 0.0f);
		float ScaleFloat = Random.Range (MinScale, MaxScale);
		transform.localScale *= ScaleFloat;
		SpinFactor = Random.Range (0.5f, 10.0f);

	}
	
	// Update is called once per frame
	void Update () {
		transform.position += MovementVector;
		Vector3 rotation = new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + SpinFactor);
		Quaternion rot = Quaternion.Euler (rotation);
		transform.rotation = rot;
		Lifetime -= Time.deltaTime;
		if(Lifetime < 0.0f){
			Destroy (gameObject);
		}
	}
}

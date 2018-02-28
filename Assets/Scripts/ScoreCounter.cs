using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

	int CurrentScore = 0;
	float ExactScore = 0.0f;

	public Text text;
	private bool Counting = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Counting){
			ExactScore += Time.deltaTime * 300;
			CurrentScore = Mathf.RoundToInt (ExactScore);
			text.text = CurrentScore.ToString();
		}
	}

	public void BeginCounting(){
		Counting = true;
	}

	public void StopCounting(){
		Counting = false;
	}

	public int GetScore(){
		return CurrentScore;
	}

	public void ResetScore(){
		ExactScore = 0.0f;
		CurrentScore = 0;
	}


}

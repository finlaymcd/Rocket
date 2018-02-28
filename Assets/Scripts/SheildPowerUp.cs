using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildPowerUp : PowerUp {

	public GameObject Shield;
	private GameObject CurrentSheild;

	public override void ActivatePowerUp(RocketShip ship){
		base.ActivatePowerUp (ship);
		CurrentSheild = Instantiate (Shield, ship.transform);
		CurrentSheild.transform.localScale *= 3;
	}
}

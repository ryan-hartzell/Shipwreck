using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public List<ShipController> ships;
	int money;


	// Use this for initialization
	void Start () {
		ships = new List<ShipController> ();
		AddShip (0, 0);
		AddShip (13.64f, 0);
		money = 0;

	}

	void AddShip (float x, float y) {
		GameObject instance = Instantiate (GameManager.instance.shipTypes[0], new Vector3 (x, y, 0f), Quaternion.identity);
		instance.transform.SetParent (this.transform);
		ships.Add (instance.GetComponent<ShipController>());
	}

	// Update is called once per frame
	void Update () {

	}

	public void EndTurn() {
		foreach (ShipController ship in ships) {
			ship.ResetRange ();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public List<ShipController> ships;
	public int money;
	public int points;


	// Use this for initialization
	void Start () {
		ships = new List<ShipController> ();
		SpawnShips (this.transform.position);
		//AddShip (0, 0);
		//AddShip (13.64f, 0);
		money = 100;
		points = 0;

	}

	void AddShip (float x, float y) {
		GameObject instance = Instantiate (GameManager.instance.shipTypes[0], new Vector3 (x, y, 0f), Quaternion.identity);
		instance.transform.SetParent (this.transform);
		ships.Add (instance.GetComponent<ShipController>());
	}

	// Update is called once per frame
	void Update () {

	}

	public void SpawnShips(Vector3 position) {
		float startX, direction;
		if (position.x > 0)
			startX = position.x - (float)(GameManager.instance.shipTypes.Length * 3);
		else
			startX = 0;

		if (position.y > 0)
			direction = 180;
		else
			direction = 1;

		for (int i = 0; i < GameManager.instance.shipTypes.Length; i++) {
			GameObject instance = Instantiate (GameManager.instance.shipTypes [i], new Vector3 (startX, position.y, 0f), Quaternion.identity);
			instance.transform.Rotate (0f, 0f, direction);
			instance.transform.SetParent (this.transform);
			ships.Add (instance.GetComponent<ShipController> ());
			startX += 3.0f;
		}
	}

	public void EndTurn() {
		foreach (ShipController ship in ships) {
			ship.ResetRange ();
		}
	}
}

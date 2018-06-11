using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {
	//private Rigidbody2D rb;
	//private float orientation = 0;
	public float speed = 0.06f;
	public float rotation_speed = 1.2f;
	public float moveRange;
	public static float range = 25.0f;
	public float moveCost = .04f;
	public int health = 100;
	public int attackDamage = 100;
	public bool inputEnabled = false;
	public bool overlayEnabled = false;
	public bool hasAttackedThisTurn = false;
	public ShipController targetShip;
	public int playerId;
	public int shipsDestroyed = 0;

	public GameObject mainCamera;


	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody2D>();
		moveRange = range;
	}

	// Update is called once per frame	
	void Update () {

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		print("enter");
		if (collision is BoxCollider2D)
		{
			GameObject ship = collision.gameObject;
			ShipController thisShipController = gameObject.GetComponent<ShipController>();
			ShipController otherShipController = ship.GetComponent<ShipController>();

			if (otherShipController != null) {
				targetShip = otherShipController;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{

		print("exit");
		if (collision is BoxCollider2D)
		{
			GameObject ship = collision.gameObject;
			ShipController thisShipController = gameObject.GetComponent<ShipController>();
			ShipController otherShipController = ship.GetComponent<ShipController>();

			if (otherShipController != null) {
				targetShip = null;
			}
		}
	}

	private void handleAttack(ShipController attacker, ShipController defender) {
		
		if (attacker.playerId != defender.playerId && !hasAttackedThisTurn && targetShip != null) {
			defender.health = defender.health - attacker.attackDamage;
			hasAttackedThisTurn = true;
			print(defender.health);
			if (targetShip.health <= 0) {
				shipsDestroyed++;
			}
		}
		else {
			print("Attack cannot happen");
		}
	}

	void FixedUpdate() {
		if (inputEnabled == true) {
			//transform.Translate (Vector3.up * 5 * Input.GetAxisRaw ("Horizontal") * Time.deltaTime);
			//transform.Translate (Vector3.right * 5 * Input.GetAxisRaw ("Vertical") * Time.deltaTime);

			if (moveRange > 0) {
				if (Input.GetKey (KeyCode.W)) {
					transform.position += (transform.up * speed);
					//mainCamera.transform.position += (mainCamera.transform.up * speed);
					moveRange -= moveCost;
				}

				if (Input.GetKey (KeyCode.A)) {
					transform.Rotate (rotation_speed * Vector3.forward);
					moveRange -= moveCost;
				} else if (Input.GetKey (KeyCode.D)) {
					transform.Rotate (rotation_speed * Vector3.back);
					moveRange -= moveCost;
				}
			}
			if (moveRange < 0) {
				moveRange = 0;
			}

			if (Input.GetKeyDown (KeyCode.K)) {
				handleAttack(this, targetShip);
			}
		}

		if (health <= 0)
			gameObject.SetActive (false);

		gameObject.transform.Find("CombatOverlay").GetComponent<Canvas>().enabled = overlayEnabled;
	}


	void ToggleMovement(){
        inputEnabled = !inputEnabled;
	}

	void ToggleOverlays() {
		overlayEnabled = !overlayEnabled;
		gameObject.GetComponent<ShrinkingCircle>().toggleVisibility();
	}

	void DisableOverlays() {
		overlayEnabled = false;
		gameObject.GetComponent<ShrinkingCircle>().disableVisibility();
	}

	public void EndTurn() {
		moveRange = range;
		hasAttackedThisTurn = false;
		shipsDestroyed = 0;
	}
}

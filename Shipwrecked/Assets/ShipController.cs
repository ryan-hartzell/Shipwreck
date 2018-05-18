using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {
	//private Rigidbody2D rb;
	//private float orientation = 0;
	private float speed = 0.05f;
	private float moves = 100.0f;
	public Text moveText;

	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		/**
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector2 movement = new Vector2(moveHorizontal, moveVertical);
		rb.AddForce(movement * speed);
		*/

		if (moves > 0) {
			if (Input.GetKey (KeyCode.W)) {
				transform.position += (transform.up * speed);
				moves -= .05f;
			}

			if (Input.GetKey (KeyCode.A)) {
				transform.Rotate (Vector3.forward);
				moves -= .1f;
			} else if (Input.GetKey (KeyCode.D)) {
				transform.Rotate (Vector3.back);
				moves -= .1f;
			}
		}

		SetText ();
	}

	void SetText() {
		moveText.text = "Movement Remaining: " + moves.ToString();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {
	//private Rigidbody2D rb;
	//private float orientation = 0;
	public float speed = 0.06f;
	public float rotation_speed = 1.2f;
	public float moveRange = 25.0f;
	public float moveCost = .04f;
	public bool inputEnabled = false;

	public GameObject mainCamera;
	public Text moveText;

	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {

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

			SetText ();
		}
	}

	void SetText() {
		moveText.text = "Movement Remaining: " + moveRange.ToString();
	}

	void ToggleMovement(){
        inputEnabled = !inputEnabled;
	}
}

using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class CameraController : MonoBehaviour {
    public Camera mainCamera;
	public GameObject player;       //Public variable to store a reference to the player game object
	//List<GameObject> ships = new List<GameObject>();
	public GameObject[] ships;
	public int active = 0;
    public bool freeMovement = false;
    public float speed = 0.06f;

	private Vector3 offset;         //Private variable to store the offset distance between the player and camera

	// Use this for initialization
	void Start () 
	{
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - player.transform.position;
		player = GameManager.instance.players [0].ships [0].gameObject;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleCameraMovement();
            player.gameObject.SendMessage("ToggleMovement");
        } 
        else if (Input.GetKeyDown(KeyCode.Equals))
        {
            mainCamera.orthographicSize -= 0.5f;
        } 
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            mainCamera.orthographicSize += 0.5f;  
        }

        if (freeMovement) {
            if (Input.GetKey(KeyCode.W)) 
            {
                transform.position += (transform.up * speed);
            } 
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position -= (transform.up * speed);
            } 

            if (Input.GetKey(KeyCode.A)) 
            {
                transform.position -= (transform.right * speed);
            } 
            else if (Input.GetKey(KeyCode.D)) 
            {
                transform.position += (transform.right * speed);
            } 
        } else {
            transform.position = player.transform.position + offset;
        }
	}

    void ToggleCameraMovement () {
        freeMovement = !freeMovement;
    }

	void SwapPlayers(){
		if (active < ships.Length-1) {
			active += 1;
		} else {
			active = 0;
		}
		//player = ships [active];
	}

	public void ChangeShip(int pl, int ship) {
		GameManager gm = GameManager.instance;
		player = gm.players [pl].ships [ship].gameObject;
	}
}
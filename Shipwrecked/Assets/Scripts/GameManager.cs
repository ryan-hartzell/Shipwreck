using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
	//List<Transform> ships = new List<Transform>();
	//public Transform[] ships;
	public int numPlayers;
	public GameObject player;
	public List<Player> players;
	public GameObject[] shipTypes;
	public int activeShip = 0;
	public int activePlayer = 0;
	public CameraController c;
	private bool setup = false;

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		//Get a component reference to the attached BoardManager script
		boardScript = GetComponent<BoardManager>();

		//Call the InitGame function to initialize the first level 
		InitGame();
	}

	//Initializes the game for each level.
	void InitGame()
	{
		//Call the SetupScene function of the BoardManager script, pass it current level number.
		boardScript.SetupScene();
		for (int i = 0; i < numPlayers; i++) {
			GameObject temp = Instantiate (player, new Vector3 (0, 0, 0), Quaternion.identity);
			players.Add (temp.GetComponent<Player>());
		}
	}

	//Update is called every frame.
	void Update()
	{
		ShipController activeShipCtrl = players[activePlayer].ships[activeShip];
		
		if (!setup) {
			setup = true;
			players [0].ships [0].inputEnabled = true;
			c.Initialize (0, 0);
		}

		if (Input.GetKeyDown(KeyCode.O)) {
			ShrinkingCircle shipCircle = activeShipCtrl.GetComponent<ShrinkingCircle>();
			shipCircle.toggleVisibility();
		}
		if(Input.GetKeyDown(KeyCode.B) && c.freeMovement == false){
			activeShipCtrl.gameObject.SendMessage ("ToggleMovement");

			if (activeShip < players [activePlayer].ships.Count - 1)
				activeShip += 1;
			else
				activeShip = 0;
			activeShipCtrl.gameObject.SendMessage ("ToggleMovement");
			c.ChangeShip (activePlayer, activeShip);
		}
		else if (Input.GetKeyDown (KeyCode.T) && c.freeMovement == false) {
			activeShipCtrl.gameObject.SendMessage ("ToggleMovement");
			players [activePlayer].EndTurn ();
			activePlayer = (activePlayer + 1) % players.Count;
			activeShipCtrl.gameObject.SendMessage ("ToggleMovement");
			c.ChangeShip (activePlayer, activeShip);
		}
	}
}
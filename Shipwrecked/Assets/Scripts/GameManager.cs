using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
	public Text moveText, playerText;

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
		float x = (float)boardScript.columns - 1;
		float y = (float)boardScript.rows - 1;
		Vector3[] positions = { new Vector3 (0, 0, 0), new Vector3 (x, y, 0), new Vector3 (0, y, 0), new Vector3 (x, 0, 0) };
		for (int i = 0; i < numPlayers; i++) {
			GameObject temp = Instantiate (player, positions[i], Quaternion.identity);
			Player newPlayer = temp.GetComponent<Player> ();

			newPlayer.playerId = i;
			players.Add (newPlayer);
		}
	}

	//Update is called every frame.
	void Update()
	{

		if (!setup)
		{
			setup = true;
			players[0].ships[0].inputEnabled = true;
			c.Initialize(0, 0);
		}

		ShipController activeShipCtrl = players[activePlayer].ships[activeShip];

		if (Input.GetKeyDown(KeyCode.O))
		{
			activeShipCtrl.gameObject.SendMessage("ToggleOverlays");
		}

		if(Input.GetKeyDown(KeyCode.B) && c.freeMovement == false){
			
			players [activePlayer].ships [activeShip].gameObject.SendMessage ("ToggleMovement");
			players[activePlayer].ships[activeShip].gameObject.SendMessage("DisableOverlays");
			activeShip = (activeShip + 1) % players [activePlayer].ships.Count;
			players [activePlayer].ships [activeShip].gameObject.SendMessage ("ToggleMovement");
			c.ChangeShip (activePlayer, activeShip);
		}
		else if (Input.GetKeyDown (KeyCode.T) && c.freeMovement == false) {
			players [activePlayer].ships [activeShip].gameObject.SendMessage ("ToggleMovement");
			players[activePlayer].ships[activeShip].gameObject.SendMessage("DisableOverlays");
			players [activePlayer].EndTurn ();
			activePlayer = (activePlayer + 1) % players.Count;
			players [activePlayer].ships [activeShip].gameObject.SendMessage ("ToggleMovement");
			c.ChangeShip (activePlayer, activeShip);
		}

		playerText.text = "Player " + (activePlayer + 1) +
		"\n\tMoney: " + players[activePlayer].money +
		"\n\tPoints: " + players[activePlayer].points +
			"\n\tCurrent Ship Health: " + players[activePlayer].ships[activeShip].health +
		"\n\tShips: " + players [activePlayer].ships.Count;
		moveText.text = "Movement Left: " + activeShipCtrl.moveRange.ToString();
	}
}
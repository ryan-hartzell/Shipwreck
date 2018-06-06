using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
	//List<Transform> ships = new List<Transform>();
	public Transform[] ships;
	public int active = 0;
	public CameraController c;

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

	}
		
	//Update is called every frame.
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.B)){
			ships[active].gameObject.SendMessage("ToggleMovement");
			if (active < ships.Length-1) {
				active += 1;
			} else {
				active = 0;
			}
			ships[active].gameObject.SendMessage("ToggleMovement");
            c.gameObject.SendMessage("SwapPlayers");
		}
	}
}
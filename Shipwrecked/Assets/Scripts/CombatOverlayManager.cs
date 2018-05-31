using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatOverlayManager : MonoBehaviour {
	
	public GameObject coneWrapperUICanvas;
	public GameObject ship;

	// Use this for initialization
	void Start () {
		coneWrapperUICanvas.transform.rotation = ship.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		coneWrapperUICanvas.transform.rotation = ship.transform.rotation;
	}
}

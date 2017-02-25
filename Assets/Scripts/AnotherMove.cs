using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherMove : MonoBehaviour {

	private Vector2 PlayerPosition;

	// Use this for initialization
	void Start () {
		PlayerPosition = this.transform.position;

		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.W)) {
			PlayerPosition.y++;
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			PlayerPosition.y--;
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			PlayerPosition.x--;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			PlayerPosition.x++;
		}

		this.transform.position = PlayerPosition; 
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	private Vector2 PlayerPosition;

	// Use this for initialization
	void Start () {
		PlayerPosition = this.transform.position;

		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			PlayerPosition.y++;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			PlayerPosition.y--;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			PlayerPosition.x--;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			PlayerPosition.x++;
		}

		this.transform.position = PlayerPosition; 
		
	}
}

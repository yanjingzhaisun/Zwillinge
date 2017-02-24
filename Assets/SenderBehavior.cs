using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action {Up, Down, Left, Right};

public class SenderBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W)) {
			CreateWave(Action.Up);
		}
		else if(Input.GetKeyDown(KeyCode.A)) {
			CreateWave(Action.Left);
		}
		else if(Input.GetKeyDown(KeyCode.S)) {
			CreateWave(Action.Down);
		}
		else if(Input.GetKeyDown(KeyCode.D)) {
			CreateWave(Action.Right);
		}
	}

	void CreateWave(Action action) {
		GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
		temp.GetComponent<Wave>().action = action;
	}
}

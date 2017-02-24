using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action {Up, Down, Left, Right};
public enum Player {One, Two};

public class SenderBehavior : MonoBehaviour {

	public Player player;
	KeyCode[] controls;
	// Use this for initialization
	void Start () {
		if(player == Player.One) 
			{
				GetComponent<SpriteRenderer>().color = Color.red;
				controls = new KeyCode[] {KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D};
			}
		else {
				GetComponent<SpriteRenderer>().color = Color.blue;
				controls = new KeyCode[] {KeyCode.I, KeyCode.K, KeyCode.J, KeyCode.L};	
			}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(controls[0])) {
			CreateWave(Action.Up);
		}
		else if(Input.GetKeyDown(controls[1])) {
			CreateWave(Action.Down);
		}
		else if(Input.GetKeyDown(controls[2])) {
			CreateWave(Action.Left);
		}
		else if(Input.GetKeyDown(controls[3])) {
			CreateWave(Action.Right);
		}
	}

	void CreateWave(Action action) {
		GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
		temp.GetComponent<Wave>().SetProperties(action, 7 + (int) player);
	}
}

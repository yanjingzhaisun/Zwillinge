using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Relay(Action action, int layer) {
		if(gameObject.layer != layer) {
			gameObject.layer = layer;
			GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
			temp.GetComponent<Wave>().action = action;
			InterpretAction(action);
		}
	}

	void InterpretAction(Action action) {
		switch(action) {
			case Action.Up:
				StartCoroutine(Move(Vector3.up, 2));
				break;
			case Action.Down:
				StartCoroutine(Move(Vector3.down, 1));
				break;
			case Action.Left:
				StartCoroutine(Move(Vector3.left, 3));
				break;
			case Action.Right:
				StartCoroutine(Move(Vector3.right, 4));
				break;
			default:
				Debug.LogError("Unverified Action came through");
				break;
		}
	}

	IEnumerator Move(Vector3 movementVector, float timeInterval) {
		float t = 0;
		while(t < timeInterval) {
			transform.position += (movementVector * Time.deltaTime);
			yield return null;
		}
	}
}

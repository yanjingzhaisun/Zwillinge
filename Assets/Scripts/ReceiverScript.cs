using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverScript : MonoBehaviour {

	bool moving;
	// Use this for initialization
	void Start () {
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Relay(Action action, int layer, float radiusSize) {
		if(gameObject.layer != layer || !moving) {
			gameObject.layer = layer;
			GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
			temp.GetComponent<Wave>().SetProperties(action, layer, radiusSize);
			InterpretAction(action);
			SetColor(layer);
		}
	}

	public void Relay(Vector2 movementVector, int layer, float radiusSize) {
		if(gameObject.layer != layer || !moving) {
			gameObject.layer = layer;
			GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
			temp.GetComponent<Wave>().SetProperties(movementVector, layer, radiusSize);
			StartCoroutine(Move(new Vector3(movementVector.x, movementVector.y, 0), 2));
			SetColor(layer);
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
		Debug.Log(movementVector);
		while(t < timeInterval) {
			moving = true;
			transform.position += (movementVector * Time.deltaTime);
			t += Time.deltaTime;
			yield return null;
		}
		moving = false;
	}

	void SetColor(int layer) {
		if(layer == 7) {
				GetComponent<SpriteRenderer>().color = Color.red;
		}
		else {
				GetComponent<SpriteRenderer>().color = Color.blue;
		}
	}
}

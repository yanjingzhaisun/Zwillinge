using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverScript : MonoBehaviour {

	public List<Sprite> ReceiverStatus;

	public bool resetReceiver;
	Queue<int> lark;
	// Use this for initialization
	void Start () {
		lark = new Queue<int>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Relay(Action action, int layer, float radiusSize) {
		if(!lark.Contains(layer)) {
			lark.Enqueue(layer);
			gameObject.layer = layer;
			SetColor(layer);
			if(resetReceiver) {
				ResetRelay(action);
				return;
			}
			GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
			temp.GetComponent<Wave>().SetProperties(action, layer, radiusSize);
			InterpretAction(action);
		}
	}

	public void Relay(Vector2 movementVector, int layer, float radiusSize) {
		if(!lark.Contains(layer)) {
			lark.Enqueue(layer);
			gameObject.layer = layer;
			SetColor(layer);
			if(resetReceiver && layer != 9) {
				ResetRelay(movementVector);
				return;
			}
			GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
			temp.GetComponent<Wave>().SetProperties(movementVector, layer, radiusSize);
			StartCoroutine(Move(new Vector3(movementVector.x, movementVector.y, 0), 2));
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
			t += Time.deltaTime;
			yield return null;
		}
		lark.Dequeue();
	}

	void SetColor(int layer) {
		if(layer == 7) {
			GetComponent<SpriteRenderer>().sprite = ReceiverStatus[1];
			GetComponent<AutoRotation>().rotationSpeed = 2f;
		}
		else if (layer == 8){
				GetComponent<SpriteRenderer>().sprite = ReceiverStatus[2];
			GetComponent<AutoRotation>().rotationSpeed = -2f;
		}
		else {
			GetComponent<SpriteRenderer>().sprite = ReceiverStatus[0];
			GetComponent<AutoRotation>().rotationSpeed = 0f;
		}
	}

	void ResetRelay(Action action) {
		lark.Enqueue(9);
		GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
		temp.GetComponent<Wave>().SetProperties(action, 9, 1);
		resetReceiver = false;
	}

	void ResetRelay(Vector2 movementVector) {
		lark.Enqueue(9);
		GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
		temp.GetComponent<Wave>().SetProperties(movementVector, 9, 1);
		resetReceiver = false;
	}
}

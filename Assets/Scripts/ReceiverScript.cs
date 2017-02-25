using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverScript : MonoBehaviour {

	public float reverser = 1f;

	public List<Sprite> ReceiverStatus;

	public float speed;

	public bool resetReceiver;
	//Queue<int> lark;
	bool[] layersInfluence = new bool[10];
	// Use this for initialization
	void Start () {
		for (int i = 0; i < layersInfluence.Length; i++)
			layersInfluence[i] = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Relay(Action action, int layer, float radiusSize) {
		if(!layersInfluence[layer]) {
			layersInfluence[layer] = true;
			gameObject.layer = layer;
			SetColor(layer);
			if(resetReceiver) {
				ResetRelay(action);
				return;
			}
			GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
			temp.GetComponent<Wave>().SetProperties(action, layer, radiusSize);
			InterpretAction(action, layer);
		}
	}

	public void Relay(Vector2 movementVector, int layer, float radiusSize) {
		if (resetReceiver)
			Debug.Log(gameObject.name);
		if ((!layersInfluence[layer]) /*&& (!layersInfluence[9])*/)
		{
			layersInfluence[layer] = true;
			gameObject.layer = layer;
			SetColor(layer);
			if ((resetReceiver && layer != 9) || layer == 9)
			{
				ResetRelay(movementVector);
				return;
			}

			GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
			temp.GetComponent<Wave>().SetProperties( 0.9f * movementVector, layer, radiusSize);
			StartCoroutine(Move(new Vector3(movementVector.x, 2 * movementVector.y, 0), 8, layer));
		}
	}

	void InterpretAction(Action action, int layer) {
		switch(action) {
			case Action.Up:
				StartCoroutine(Move(Vector3.up, 2, layer));
				break;
			case Action.Down:
				StartCoroutine(Move(Vector3.down, 1, layer));
				break;
			case Action.Left:
				StartCoroutine(Move(Vector3.left, 3, layer));
				break;
			case Action.Right:
				StartCoroutine(Move(Vector3.right, 4, layer));
				break;
			default:
				Debug.LogError("Unverified Action came through");
				break;
		}
	}

	IEnumerator Move(Vector3 movementVector, float timeInterval, int layerInfo) {
		reverser = 1;
		if (layerInfo != 9)
			AudioDirector.instance.PlaySFX();
		else AudioDirector.instance.PlayReset();

		Vector3 orthoDir =  Vector3.Cross(movementVector, new Vector3(0, 0, 1f)).normalized;
		movementVector += orthoDir * Random.Range(-0.3f, 0.3f) * movementVector.magnitude;

		float t = 0;
		while(t < timeInterval) {
			transform.position += (movementVector * Time.deltaTime * speed * reverser);
			t += Time.deltaTime;
			yield return null;
		}
		layersInfluence[layerInfo] = false;
		//Debug.Log("<b>REceiverScript</b>: out " + gameObject.name + " " + lark.Count);
	}

	void SetColor(int layer) {
		if(layer == 7) {
			GetComponent<SpriteRenderer>().sprite = ReceiverStatus[1];

			GetComponent<AutoRotation>().ChangeRotation(2f);
		}
		else if (layer == 8){
				GetComponent<SpriteRenderer>().sprite = ReceiverStatus[2];
			GetComponent<AutoRotation>().ChangeRotation(-2f);
		}
		else {
			GetComponent<SpriteRenderer>().sprite = ReceiverStatus[0];
			GetComponent<AutoRotation>().ChangeRotation(0f);
		}
	}

	void ResetRelay(Action action) {
		//lark.Enqueue(9);
		GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
		temp.GetComponent<Wave>().SetProperties(action, 9, 1);
		resetReceiver = false;
	}

	void ResetRelay(Vector2 movementVector) {
		//lark.Enqueue(9);
		SetColor(9);
		for (int i = 0; i < layersInfluence.Length; i++)
			layersInfluence[i] = false;
		layersInfluence[9] = true;
		
		GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
		Vector3 dir3 = Random.onUnitSphere;
		Vector2 dir = (new Vector2(dir3.x, dir3.y));

		temp.GetComponent<Wave>().SetProperties(dir, 9, 0.5f);
		resetReceiver = false;


		StartCoroutine(Move(new Vector3(movementVector.x, movementVector.y, 0), 4, 9));
	}
}

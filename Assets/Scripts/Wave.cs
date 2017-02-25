using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	public float expansionSpeed;
	private float size;
	private float changeSpeed = 1.01f;
	public static float baseRadius = 0.8f;
	private float targetRadius = baseRadius;
	SpriteRenderer spriteRenderer;
	public Color player1Color;
	public Color player2Color;
	public Color resetColor;
	public Action action;
	public Vector2 movementVector;
	public int layer;
	// Use this for initialization

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		size = 0;
	}
	
	// Update is called once per frame
	void Update () {
		size += (Time.deltaTime * changeSpeed);
		RescaleObject();

	}

	void RescaleObject() {
		transform.localScale = new Vector3(0.1f + size, 0.1f + size, 0.1f + size);
		Color newColor = spriteRenderer.color;
		newColor.a = 1 - (0.1f + size / targetRadius);//(targetRadius - 1 - size) / targetRadius;
		if(newColor.a < 0) {
			Destroy(gameObject);
		}
		spriteRenderer.color = newColor;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Receiver")
		{
			//Debug.Log("<b>Wave</b>: Trigger enter -> " + other.gameObject.name);
			float targetSize = targetRadius - size;
			if (layer == 9)
			{
				targetSize = 1;
			}
#if DISCRETEINPUT
			other.gameObject.gameObject.GetComponent<ReceiverScript>().Relay(action, layer);
#else
			other.gameObject.gameObject.GetComponent<ReceiverScript>().Relay(movementVector, layer, targetSize);
#endif
		}
		else if (other.tag == "Player") {
			if (layer == 9) {
				other.gameObject.GetComponent<SenderBehavior>().Stunned = true;
			}
		}
	}

	public void SetProperties(Action newAction, int newLayer, float newRadius) {
		action = newAction;
		layer = newLayer;
		targetRadius = newRadius;
	}

	public void SetProperties(Vector2 newMovementVector,int newLayer, float newRadius) {
		movementVector = newMovementVector;
		layer = newLayer;
		targetRadius = newRadius;
		SetColor();
	}

	void SetColor() {
		if(layer == 7) {
			spriteRenderer.color = player1Color;
		}
		else if(layer == 8) {
			spriteRenderer.color = player2Color;
		}
		else {
			spriteRenderer.color = resetColor;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	public float expansionSpeed;
	private float size;
	private float alpha = 1;
	private float changeSpeed = 1.2f;
	public static float baseRadius = 0.5f;
	private float targetRadius = baseRadius;
	Color startColor;
	SpriteRenderer renderer;
	public Action action;
	public Vector2 movementVector;
	public int layer;
	// Use this for initialization

	void Awake () {
		renderer = GetComponent<SpriteRenderer>();
		size = 0;
		startColor = GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
		size += (Time.deltaTime * changeSpeed);
		RescaleObject();

	}

	void RescaleObject() {
		transform.localScale = new Vector3(0.1f + size, 0.1f + size, 0.1f + size);
		Color newColor = renderer.color;
		newColor.a = (targetRadius - 1 - size) / targetRadius;//Time.deltaTime * 0.3f);
		if(newColor.a < 0) {
			Destroy(gameObject);
		}
		renderer.color = newColor;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Receiver") {
			float targetSize = targetRadius - size;
			if(layer == 9) {
				targetSize = 5;
			}
			#if DISCRETEINPUT
			other.gameObject.gameObject.GetComponent<ReceiverScript>().Relay(action, layer);
			#else
			other.gameObject.gameObject.GetComponent<ReceiverScript>().Relay(movementVector, layer, targetSize);
			#endif

		}
	}

	public void SetProperties(Action action, int newLayer, float newRadius) {
		action = action;
		layer = newLayer;
		targetRadius = newRadius;
	}

	public void SetProperties(Vector2 newMovementVector,int newLayer, float newRadius) {
		movementVector = newMovementVector;
		layer = newLayer;
		targetRadius = newRadius;
	}
}

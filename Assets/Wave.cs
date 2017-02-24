using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	public float expansionSpeed;
	private float size;
	private float alpha = 1;
	private float changeSpeed = 5;
	Color startColor;
	SpriteRenderer renderer;
	public Action action;
	// Use this for initialization

	void Start () {
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
		transform.localScale = new Vector3(1 + size, 1 + size, 1 + size);
		Color newColor = renderer.color;
		newColor.a -= (Time.deltaTime * 0.3f);
		if(newColor.a < 0) {
			Destroy(gameObject);
		}
		renderer.color = newColor;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Receiver") {
			other.gameObject.gameObject.GetComponent<ReceiverScript>().Relay(action, 8);
		}
	}
}

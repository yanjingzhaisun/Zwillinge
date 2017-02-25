using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManagerScript : MonoBehaviour {

	public float timeInterval;
	private float timer;
	FieldBehavior p1Side, p2Side;
	Transform p1Field, p2Field;

	// Use this for initialization
	void Start () {
		timer = timeInterval;
		p1Field = transform.GetChild(0);
		p2Field = transform.GetChild(1);
		p1Field.gameObject.layer = 7;
		p2Field.gameObject.layer = 8;
		p1Side = p1Field.gameObject.GetComponent<FieldBehavior>();
		p2Side = p2Field.gameObject.GetComponent<FieldBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0) {
			Debug.Log("tug o war");
			timer = timeInterval;
			int p1Entries = p1Side.numEntries();
			int p2Entries = p2Side.numEntries();
			if(p1Entries > p2Entries) {
				ScaleFields(p1Field, p2Field);
			}
			else if(p2Entries > p1Entries) {
				ScaleFields(p2Field, p1Field);
			}
		}
	}

	void ScaleFields(Transform biggerField, Transform smallerField) {
		Vector3 temp = biggerField.localScale;
		temp += new Vector3(10, 0, 0);
		biggerField.localScale = temp;
		temp = smallerField.localScale;
		temp -= new Vector3(-10, 0, 0);
		smallerField.localScale = temp;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Receiver") {
			Debug.Log("receiver found");
			col.gameObject.GetComponent<ReceiverScript>().reverser = -1;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBehavior : MonoBehaviour {

	List<GameObject> insideEntities;
	// Use this for initialization
	void Start () {
		insideEntities = new List<GameObject>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		insideEntities.Add(other.gameObject);
	}

	void OnTriggerExit2D(Collider2D other) {
		insideEntities.Remove(other.gameObject);
	}

	public int numEntries() {
		int total = 0;
		foreach(GameObject obj in insideEntities) {
			if(obj.layer == gameObject.layer) {
				total++;
			}
		}

		return total;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

	public GameObject[] players;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 vect = (players[0].transform.position + players[1].transform.position) / 2;
		transform.position = Vector3.Lerp(transform.position, new Vector3(vect.x, vect.y, transform.position.z), 0.5f);
		float distance = Vector3.Distance(players[0].transform.position, players[1].transform.position);
		Camera.main.orthographicSize = Mathf.Clamp(Mathf.Lerp(Camera.main.orthographicSize, distance / 2f, 0.5f), 4f, 10f);
	}
}

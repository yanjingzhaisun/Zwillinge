using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {

	public float width;
	public float height;
	public int numReceivers;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < numReceivers; i++) {
			float randomX = (Random.value * width) - (width / 2);
			float randomY = (Random.value * height) - (height / 2);
			Instantiate(Resources.Load<GameObject>("Receiver"), new Vector3(randomX, randomY, 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

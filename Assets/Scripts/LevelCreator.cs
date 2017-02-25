using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {

	public float width;
	public float height;
	public int numReceivers;
	public int numScoreAreas;
	public static float chanceOfReset = 0.01f;

	public List<ReceiverScript> receivers;
	// Use this for initialization
	void Start () {
		CreateObjects("Receiver", numReceivers);
		CreateObjects("Area", numScoreAreas);
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Receiver")) {
			if(Random.value < chanceOfReset) {
				obj.GetComponent<ReceiverScript>().resetReceiver = true;
				Debug.Log("made a receiver");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateObjects(string objectName, int count) {
		for(int i = 0; i < count / 2; i++) {
			float randomX = (Random.value * -width / 2);
			float randomY = (Random.value * height) - (height / 2);
			GameObject go = Instantiate(Resources.Load<GameObject>(objectName), new Vector3(randomX, randomY, 0), Quaternion.identity);
			go.name = "Receiver " + i;
		}

		for(int i = count / 2; i < count; i++) {
			float randomX = (Random.value * width / 2);
			float randomY = (Random.value * height) - (height / 2);
			GameObject go = Instantiate(Resources.Load<GameObject>(objectName), new Vector3(randomX, randomY, 0), Quaternion.identity);
		}
	}
}

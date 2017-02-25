using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {

	public float width;
	public float height;
	public int numReceivers;
	public int numScoreAreas;
	public float chanceOfReset;

	public List<ReceiverScript> receivers;
	// Use this for initialization
	void Start () {
		/*for(int i = 0; i < numReceivers; i++) {
			float randomX = (Random.value * width) - (width / 2);
			float randomY = (Random.value * height) - (height / 2);
			Instantiate(Resources.Load<GameObject>("Receiver"), new Vector3(randomX, randomY, 0), Quaternion.identity);
		}
		for(int i = 0; i < numScoreAreas; i++) {
			
		}*/
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
		for(int i = 0; i < count; i++) {
			float randomX = (Random.value * width) - (width / 2);
			float randomY = (Random.value * height) - (height / 2);
			GameObject go = Instantiate(Resources.Load<GameObject>(objectName), new Vector3(randomX, randomY, 0), Quaternion.identity);
			go.name = "Receiver " + i;
		}
	}
}

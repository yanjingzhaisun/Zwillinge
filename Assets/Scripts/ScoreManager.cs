using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public GameObject scoretext;
	public new float score;
	new int count;
	new float time;

	// Use this for initialization
	void Start () {
		time = 0f;
		score = 0f;
		count = 0;
		
	}
	
	// Update is called once per frame
	void Update () {


		if (time >= 1f) {
			score = (score + 1f*count);
			time = 0f;
		}

		//scoretext.GetComponent<TextMesh> ().text = "Score:" + score;
		
	}

	void OnCollisionEnter2D (Collision2D col){
		count = count + 1;
		Debug.Log (count);
	}

	void OnCollisionStay2D(Collision2D col){

		time += Time.deltaTime;


	}

	void OnCollisionExit2D(Collision2D col){
		count = count - 1;
	}
	

		
		

}

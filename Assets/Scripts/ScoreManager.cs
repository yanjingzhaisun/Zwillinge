using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text p1ScoreText;
	public Text p2ScoreText;
	private List<GameObject> enclosedReceivers;
	public static int p1Score;
	public static int p2Score;
	int count;
	float time;

	// Use this for initialization
	void Awake () {
		enclosedReceivers = new List<GameObject>();
		time = 0f;
		p1Score = 0;
		p2Score = 0;
		count = 0;
		p1ScoreText = GameObject.Find("p1Score").GetComponent<Text>();
		p2ScoreText = GameObject.Find("p2Score").GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {


		if (time >= 1f && enclosedReceivers.Count > 0) {
			p1Score = p1Score + TallyScore(7);
			p2Score = p2Score + TallyScore(8);
			time = 0f;
		}

		p1ScoreText.text = "P1: " + p1Score.ToString();
		p2ScoreText.text = "P2: " + p2Score.ToString();
		//scoretext.GetComponent<TextMesh> ().text = "Score:" + score;
		
	}

	void OnCollisionEnter2D (Collision2D col){
		count = count + 1;
		enclosedReceivers.Add(col.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		enclosedReceivers.Add(other.gameObject);
	}

	void OnCollisionStay2D(Collision2D col){
		
		time += Time.deltaTime;
	}

	void OnTriggerStay2D(Collider2D other) {
		time += Time.deltaTime;
	}

	void OnCollisionExit2D(Collision2D col){
		count = count - 1;
		enclosedReceivers.Remove(col.gameObject);
	}

	void OnTriggerExit2D(Collider2D other) {
		
		enclosedReceivers.Remove(other.gameObject);
	}

	int TallyScore(int layer) {
		int total = 0;
		for(int i = 0; i < enclosedReceivers.Count; i++) {
			if(enclosedReceivers[i] != null && enclosedReceivers[i].layer == layer) {
				total++;
			}
		}
		return total;
	}
		

}

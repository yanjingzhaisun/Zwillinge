using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalScore : MonoBehaviour {

	public GameObject Area1;
	public GameObject Area2;
	public GameObject Area3;
	public GameObject scoretext;
	new float totalscore;
	new float score1;
	new float score2;
	new float score3;

	// Use this for initialization
	void Start () {
		totalscore = 0f;

		
	}
	
	// Update is called once per frame
	void Update () {
		
		score1 = Area1.GetComponent<ScoreManager>().score;
		score2 = Area2.GetComponent<ScoreManager>().score;
		score3 = Area3.GetComponent<ScoreManager>().score;

		Debug.Log("score1:"+score1+"score2:"+score2+"score3"+score3);

		totalscore = score1 + score2 + score3;

		scoretext.GetComponent<TextMesh> ().text = "Score:" + totalscore;
	}
}

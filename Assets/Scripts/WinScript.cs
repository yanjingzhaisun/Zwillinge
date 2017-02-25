using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour {

	public static bool player1;
	public GameObject player1Obj;
	public GameObject player2Obj;

	// Use this for initialization
	void Start() {
		player1Obj.SetActive(player1);
		player2Obj.SetActive(!player1);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire2" )){
			SceneManager.LoadScene("title");
		}
	}
}

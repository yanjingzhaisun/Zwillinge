using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		transform.position += new Vector3(Mathf.Sin(Time.time * 27) * 0.01f, 0, 0);
		timer -= Time.deltaTime;
		if(timer < 0) {
			timer = timeInterval;
			int p1Entries = p1Side.numEntries();
			int p2Entries = p2Side.numEntries();
			Vector3 movement = Vector3.zero;
			if(p1Entries > p2Entries) {
				movement.x = 3;
			}
			else if(p2Entries > p1Entries) {
				movement.x = -3f;
			}
			StartCoroutine(MoveBackground(p1Field.transform, movement));
			StartCoroutine(MoveBackground(p2Field.transform, movement));
			if(p1Field.localPosition.x < -7) {
				AudioDirector.instance.PlayFinal();
				StartCoroutine(GameOver());
				Time.timeScale = 0;
				WinScript.player1 = false;
			}
			else if(p2Field.localPosition.x > 7) {
				StartCoroutine(GameOver());
				AudioDirector.instance.PlayFinal();
				Time.timeScale = 0;
				Debug.Log("player 1 wins");
				WinScript.player1 = true;
			}
		}
	}

	void ScaleFields(Transform biggerField, Transform smallerField) {
		Vector3 temp = biggerField.localScale;
		temp += new Vector3(10, 0, 0);
		biggerField.localScale = temp;
		temp = smallerField.localScale;
		temp -= new Vector3(10, 0, 0);
		smallerField.localScale = temp;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Receiver") {
			col.gameObject.GetComponent<ReceiverScript>().reverser = -1;
		}
	}

	IEnumerator MoveBackground(Transform sprite, Vector3 movementAmount) {
		Vector3 startPos = sprite.position;
		Vector3 endPos = sprite.position + movementAmount;
		AudioDirector.instance.PlayMoveWall();
		float t = 0;
		while(t < 1) {
			
			sprite.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, t));
			t += Time.deltaTime;
			yield return null;
		}
	}

	IEnumerator GameOver() {
		GameObject player1 = GameObject.FindGameObjectsWithTag("Player")[0];
		GameObject player2 = GameObject.FindGameObjectsWithTag("Player")[1];

		/*player1.GetComponent<TrailRenderer>().enabled = true;
		player2.GetComponent<TrailRenderer>().enabled = true;*/
		Vector3 p1StartPos = player1.transform.position;
		Vector3 p2StartPos = player2.transform.position;
		Vector3 p1EndPos = new Vector3(-14, 0, 0);
		Vector3 p2EndPos = new Vector3(14, 0, 0);
		float t = 0;
		while(t < 2) {
			player1.transform.position = Vector3.Lerp(p1StartPos, p1EndPos, t/2f);
			player2.transform.position = Vector3.Lerp(p2StartPos, p2EndPos, t/2f);
			t += Time.unscaledDeltaTime;
			yield return null;
		}
		AudioDirector.instance.PlaySnapshots(0);
		yield return new WaitForSecondsRealtime(4);
		SceneManager.LoadScene("Scene/EndScene");
	}
}

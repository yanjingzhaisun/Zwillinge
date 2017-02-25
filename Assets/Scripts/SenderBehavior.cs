using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum Action {Up, Down, Left, Right};
public enum Player {One, Two};

public class SenderBehavior : MonoBehaviour {

	public Player player;
	public float speed;
	public float shotCooldownTime;
	private float coolDownTimer;
	private string fireButton;

	GameObject baseEmitter;
	GameObject neutralEmitter;
	GameObject coolDownIndicator;

	public float stunTime = 2f;
	float _stunTimeCountDown = -1f;

	bool _stunned = false;
	public bool Stunned {
		get {
			return _stunned;
		}
		set {
			if (_stunned != value) {
				if (value)
					OnStun();
				else
					OnResume();
			}
			_stunned = value;
		}
	}

	List<Color> colorStatus;

	KeyCode[] controls;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		baseEmitter = transform.GetChild(0).gameObject;
		neutralEmitter = transform.GetChild(1).gameObject;
		coolDownIndicator = baseEmitter.transform.GetChild(1).gameObject;
		neutralEmitter.SetActive(false);
		if(player == Player.One) 
			{
				fireButton = "Fire0";
				GetComponent<SpriteRenderer>().color = Color.red;
				controls = new KeyCode[] {KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D};
			}
		else {
				fireButton = "Fire1";
				GetComponent<SpriteRenderer>().color = Color.blue;
				controls = new KeyCode[] {KeyCode.I, KeyCode.K, KeyCode.J, KeyCode.L};	
			}
		colorStatus = new List<Color>();
		GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(p => colorStatus.Add(p.color));
		coolDownTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (_stunTimeCountDown > 0)
		{
			_stunTimeCountDown -= Time.deltaTime;
			return;
		}
		else{
			Stunned = false;
			coolDownIndicator.SetActive(true);
		}

		transform.Translate(GetMyInput("Horizontal") * speed * Time.deltaTime, GetMyInput("Vertical") * speed * Time.deltaTime, 0);
		if(Input.GetButtonDown(fireButton) && coolDownTimer <= 0) {
			//Debug.Log(fireButton);
			CreateWave(new Vector2(GetMyInput("AimHorizontal"), GetMyInput("AimVertical")));
			coolDownTimer = shotCooldownTime;
		}
		if(Input.GetKeyDown(controls[0])) {
			CreateWave(Action.Up);
		}
		else if(Input.GetKeyDown(controls[1])) {
			CreateWave(Action.Down);
		}
		else if(Input.GetKeyDown(controls[2])) {
			CreateWave(Action.Left);
		}
		else if(Input.GetKeyDown(controls[3])) {
			CreateWave(Action.Right);
		}
		if(coolDownTimer > 0) {
			coolDownIndicator.SetActive(false);
		}
		else {
			coolDownIndicator.SetActive(true);
		}
		coolDownTimer -= Time.deltaTime;
	}

	void CreateWave(Action action) {
		GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
		temp.GetComponent<Wave>().SetProperties(action, 7 + (int) player, Wave.baseRadius);
	}

	void CreateWave(Vector2 movementVector) {
		//Debug.Log("Create Wave movementVector " + movementVector.ToString());
		GameObject temp = Instantiate(Resources.Load<GameObject>("Wave"), transform.position, Quaternion.identity);
		temp.GetComponent<Wave>().SetProperties(movementVector, 7 + (int)player, Wave.baseRadius, true);
	}

	float GetMyInput(string axisName) {
		return Input.GetAxis(axisName + ((int)player).ToString());
	}

	public void OnStun() {
		_stunTimeCountDown = stunTime;
		//GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(p => p.color = new Color32(0x5C, 0x5C, 0x5C, 0xFF));
		baseEmitter.gameObject.SetActive(false);
		neutralEmitter.gameObject.SetActive(true);
	}
	public void OnResume() {
		/*int i = 0;
		GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(p => {
			p.color = colorStatus[i];
			i++;
		});*/
		baseEmitter.gameObject.SetActive(true);
		neutralEmitter.gameObject.SetActive(false);
	}
}

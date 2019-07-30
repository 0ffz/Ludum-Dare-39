using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WorkManager : MonoBehaviour {

	public GameObject RobotInstance;
	public GameObject Desk;
	public GameObject Robot;
	public float remainingSwitches;
	public float padsLeft = 1;
	public float originalPads;
	public GameObject convincingRobot;

	public float timer = 0;
	public int score = 0;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (Desk != null && Robot != null) {
			if (Robot.GetComponent<Robot> ().target != null) {
				Robot.GetComponent<Robot> ().target.GetComponent<Desk>().StopWork();
			}

			Robot.GetComponent<Robot> ().navigate.Kill ();
			Robot.GetComponent<Robot> ().target = Desk;
			Desk.GetComponent<Desk> ().robot = Robot.GetComponent<Robot>();
			Robot.GetComponent<Robot> ().StartNavigation();
		}
		if (Input.GetMouseButtonDown (1)) {
			Desk = null;
			Robot = null;
		}
		if (padsLeft <= 0) {
			convincingRobot.GetComponent<Robot> ().ConvinceSuccess();
		}
		if (timer > 30 && timer < 31) {
			Instantiate (RobotInstance, new Vector3 (-4.75f, 0.84f, 0f), transform.rotation);
			timer+= 1;
		}
		if (timer > 60 && timer < 61) {
			Instantiate (RobotInstance, new Vector3 (-4.75f, 0.84f, 0f), transform.rotation);
			timer+= 1;
		}
		if (timer > 90 && timer < 91) {
			Instantiate (RobotInstance, new Vector3 (-4.75f, 0.84f, 0f), transform.rotation);
			timer+= 1;
		}
		if (timer > 120 && timer < 121) {
			Instantiate (RobotInstance, new Vector3 (-4.75f, 0.84f, 0f), transform.rotation);
			timer+= 1;
		}
		if (timer > 150 && timer < 151) {
			Instantiate (RobotInstance, new Vector3 (-4.75f, 0.84f, 0f), transform.rotation);
			timer+= 1;
		}
		
	}
}

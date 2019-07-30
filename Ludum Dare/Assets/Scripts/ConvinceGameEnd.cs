using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvinceGameEnd : MonoBehaviour {
	
	WorkManager workManager;

	// Use this for initialization
	void Start () {
		workManager = GameObject.Find("Work Manager").GetComponent<WorkManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (workManager.padsLeft <= 0) {
			workManager.convincingRobot.GetComponent<Robot> ().ConvinceSuccess();
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter2D (Collider2D col){
		print("Entered Trigger!");
	}
}

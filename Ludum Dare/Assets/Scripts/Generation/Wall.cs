using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	WorkManager workManager;
	// Use this for initialization
	void Start(){
		workManager = GameObject.Find ("Work Manager").GetComponent<WorkManager> ();
	}
	void OnMouseOver(){
		if(workManager.padsLeft < workManager.originalPads)
			workManager.convincingRobot.GetComponent<Robot> ().ConvinceFail();
	}
}

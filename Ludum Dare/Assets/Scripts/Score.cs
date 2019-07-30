using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Text> ().text = "Hi scr : " + PlayerPrefs.GetInt("highscore");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour {

	public Sprite deactivated;

	WorkManager workManager;
	bool active = true;

	SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		workManager = GameObject.Find("Work Manager").GetComponent<WorkManager> ();
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0) && active == true) {
			active = false;
			sprite.sprite = deactivated;
			workManager.padsLeft -= 1;
		}
	}
}

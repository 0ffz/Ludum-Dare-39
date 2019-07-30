using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Convince : MonoBehaviour {
	
	public GameObject Battery;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void InstantiateIcon(){
		if (GameObject.FindGameObjectWithTag ("ConvinceIcon") == null) {
			Instantiate (Battery, new Vector3 (transform.position.x, transform.position.y, -8), transform.rotation);
		}
	}
}

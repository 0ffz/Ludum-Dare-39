using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvinceIcon : MonoBehaviour {

	public LayerMask layers;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x-0.15f, Camera.main.ScreenToWorldPoint(Input.mousePosition).y-0.15f);
		if (Input.GetMouseButtonDown (1)) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, 100, layers);
			if (hit.collider != null && hit.collider.tag == "Robot") {
				hit.collider.GetComponent<Robot> ().StartConvinceGame ();

			}
			Destroy (gameObject);
		}
		
	}


}

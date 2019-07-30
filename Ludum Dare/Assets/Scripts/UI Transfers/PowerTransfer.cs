using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTransfer : MonoBehaviour {

	public float storedPower;
	public Sprite battery1;
	public Sprite battery2;
	public Sprite battery3;
	SpriteRenderer sprite;
	public LayerMask layers;

	Power powerAmount;
	WorkManager workManager;

	// Use this for initialization
	void Start () {
		sprite = gameObject.GetComponent<SpriteRenderer> ();
		powerAmount = GameObject.Find ("Power Amount").GetComponent<Power> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		if (Input.GetMouseButtonDown (1)) {
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, layers);
			if (hit.collider != null && hit.collider.tag == "Robot") {
				hit.collider.GetComponent<Robot> ().power += 34;
				storedPower -= 1;
			} else {
				GameObject.Find ("Power Amount").GetComponent<Power> ().systemPower += storedPower * 10;
				Destroy (gameObject);
			}





		}

		if(storedPower <= 0){
			Destroy (gameObject);
		}

		if (storedPower == 1)
			sprite.sprite = battery1;
		else if (storedPower == 2) 
			sprite.sprite = battery2;
		else
			sprite.sprite = battery3;
		
	}


}

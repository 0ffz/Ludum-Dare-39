using UnityEngine;
using System.Collections;

// PlayerScript requires the GameObject to have a Rigidbody2D component

[RequireComponent (typeof (Rigidbody2D))]

public class PlayerController : MonoBehaviour {


	public float playerSpeed = 4f;
	Animator ani;

	void Start () {
		ani = gameObject.GetComponent<Animator> ();
	}

	void FixedUpdate () {
		float Horizontal = Input.GetAxisRaw ("Horizontal");
		float Vertical = Input.GetAxisRaw ("Vertical");

		Vector2 targetVelocity = new Vector2(Horizontal, Vertical);
		GetComponent<Rigidbody2D>().velocity=targetVelocity * playerSpeed;

		//Running
		if (Horizontal == 1) {
			ani.Play ("Running right");
			ani.SetBool ("isRunning", true);
		} else if (Horizontal == -1) {
			ani.Play ("Running left");
			ani.SetBool ("isRunning", true);
		} else if (Vertical == 1) {
			ani.Play ("Running back");
			ani.SetBool ("isRunning", true);
		} else if (Vertical == -1) {
			ani.Play ("Running front");
			ani.SetBool ("isRunning", true);
		} else {
			ani.SetBool ("isRunning", false);
		}

		/*if(Input.GetKeyUp(KeyCode.D)){
			ani.Play ("Bix_IR");
		}
		if(Input.GetKeyUp(KeyCode.A)){
			ani.Play ("Bix_IL");
		}
		if(Input.GetKeyUp(KeyCode.W)){
			ani.Play ("Bix_IB");
		}
		if(Input.GetKeyUp(KeyCode.S)){
			ani.Play ("Bix_IF");
		}*/

		if (Input.GetKey (KeyCode.LeftShift)) {
			playerSpeed = 7f;
			ani.speed = 1.5f;
		} else {
			playerSpeed = 4f;
			ani.speed = 1.0f;
		}
		
	}
}
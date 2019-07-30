using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour {

	public bool isComing = false;
	public bool isWorking = false;
	public float maxWorkTime;
	public int workType;
	public float reloadTime;
	public float addPowerAmount;
	public int deskID;
	public Robot robot;
	public bool animate = false;

	Color color;
	SpriteRenderer sprite;
	float workTime = 0;
	float workForce;
	WorkManager workManager;
	Transform progress;
	Power systemPower;
	SpriteRenderer childColor;
	Color progressColor;
	//Animator _a;

	void Start () {
		//_a = gameObject.GetComponent<Animator>();
		systemPower = GameObject.Find ("Power Amount").GetComponent<Power>();
		sprite = transform.Find("Sprite texture").gameObject.GetComponent<SpriteRenderer>();
		color = sprite.color;
		progress = transform.Find ("Progress");
		childColor = progress.GetComponent<SpriteRenderer> ();
		progressColor = childColor.color;
		workManager = GameObject.Find ("Work Manager").GetComponent<WorkManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isWorking == false) {
			progressColor.r = 1;
			childColor.color = progressColor;
		}else{
			progressColor.r = 0.31f;
			childColor.color = progressColor;
		}
		if (isWorking == true && robot.prefferedDeskID == deskID) {
			workTime += workForce * Time.deltaTime;
			if (transform.Find("Sprite texture").GetComponent<Animator> () != null) {
				transform.Find("Sprite texture").GetComponent<Animator> ().SetBool ("Animate", true);
			}
		}
		if (deskID == 1 && isWorking == true) {
			robot._a.SetBool ("isRunning", false);
			robot._a.Play ("Idle left");
		} else if (deskID == 4 && isWorking == true) {
			robot.animateMoving = true;
			robot._a.SetBool ("isRunning", true);
			robot._a.Play ("Running front");
		} else if (isWorking == true) {
			robot._a.SetBool ("isRunning", false);
			robot._a.Play ("Idle right");
		}


		if (workTime >= maxWorkTime) {
			workTime = 0;
			if (deskID != 3 || deskID != 4 || deskID == 1) {
				systemPower.systemPower += addPowerAmount;
			}if (deskID == 1) {
				workManager.score += 30;
				StopWork ();
				robot.Animate (1);
				robot.LowerConvinceTimes();
			}else if (deskID == 3) {
				workManager.score += 30;
				StopWork ();
				robot.Animate (3);
				robot.LowerConvinceTimes();
			}else if (deskID == 4) {
				workManager.score += 30;
				StopWork ();
				robot.Animate (4);
				robot.LowerConvinceTimes();
			}
		}
		progress.localScale = new Vector2(workTime/maxWorkTime, progress.localScale.y);
		if (isWorking == false && workTime > 0) {
			workTime -= reloadTime * Time.deltaTime;
		}
	}

	public void Work(float workForce){
		isWorking = true;
		this.workForce = workForce;
	}

	public void StopWork(){
		isWorking = false;
		isComing = false;
	}

	void OnMouseOver(){
		color = sprite.color;
		color.r = 0.9f;
		color.b = 0.9f;
		sprite.color = color;
		if (Input.GetMouseButtonDown (1) && isComing == false) {
			workManager.Desk = gameObject;
		}
	}
	void OnMouseExit(){
		color = sprite.color;
		color.r = 1f;
		color.b = 1f;
		sprite.color = color;
	}
}

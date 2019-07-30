using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Robot : MonoBehaviour {

	public GameObject target;
	public bool isSelected;
	public float workForce;
	public float speed;
	public float powerReduction;
	public Sprite battery3, battery2, battery1, battery0;
	public GameObject explosion;
	public float power = 100f;
	public int prefferedDeskID;
	public Animator _a;
	//public float Anger = 5f;
	public bool animateMoving = false;
	public LayerMask layers;

	GameObject bubble;
	Color color;
	Desk desk;
	SpriteRenderer sprite;
	WorkManager workManager;
	PolygonCollider2D polyCollider;
	SpriteRenderer BatterySprite;
	Rigidbody2D body;
	float arrived;
	public Sequence navigate;
	float directionX;
	float directionY;
	float lastPosX;
	float lastPosY;
	bool Dead = false;
	float prefferedSwitchCounter = 0.5f;
	int convinceTimes = 2;
	ArrayList randomList;

	void Start () {
		_a = gameObject.GetComponent<Animator> ();
		randomList = new ArrayList ();
		body = gameObject.GetComponent<Rigidbody2D> ();
		BatterySprite = transform.Find ("Battery").GetComponent<SpriteRenderer> ();;
		lastPosX = transform.position.x;
		lastPosY = transform.position.y;

		workManager = GameObject.Find ("Work Manager").GetComponent<WorkManager>();
		sprite = gameObject.GetComponent<SpriteRenderer>();
		color = sprite.color;
		polyCollider = gameObject.GetComponent<PolygonCollider2D> ();
		transform.DOMove (new Vector2 (0, 0.3f), 2f).SetEase(Ease.Linear);
	}














	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, 100, layers);
			if (hit.collider != null && hit.collider.gameObject == gameObject) {
				if (bubble != null) {
					Destroy (bubble, 3f);
				}
				//if (Input.GetMouseButtonDown (0)) {
					color = sprite.color;
					color.r = 0.8f;
					color.b = 0.8f;
					sprite.color = color;
					isSelected = true;
					workManager.Robot = gameObject;
				//}
			}
		}
		if (transform.position.x == -4.8f)
			Destroy (gameObject);

		prefferedSwitchCounter -= Time.deltaTime;
		if (prefferedSwitchCounter <= 0) {
			Switch (-1);
			prefferedSwitchCounter = Random.Range (20f, 40.2f);
		}
		//Right click targer
		if (Input.GetMouseButtonDown (1) && isSelected == true) {

			color = sprite.color;
			color.r = 1f;
			color.b = 1f;
			sprite.color = color;
			isSelected = false;
		}
		//Start Working
		if (target != null && transform.position == new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z) && desk.deskID == prefferedDeskID) {
			desk.Work (workForce);
			if (desk.deskID == 1 || desk.deskID == 3 || desk.deskID == 4)
				prefferedDeskID = desk.deskID;

		}
		//Removing Power over time
		power -= powerReduction*Time.deltaTime+(Random.Range(-0.1f, 0.1f)*Time.deltaTime);
		if (power > 100)
			power = 100;


		//Battery rendering
		if (power > 66)
			BatterySprite.sprite = battery3;
		else if (power <= 66 && power > 33)
			BatterySprite.sprite = battery2;
		else if (power <= 33 && power > 10)
			BatterySprite.sprite = battery1;
		else if (power <= 10)//TODO figure out a way to make the battery flash
			BatterySprite.sprite = battery0;

		//Death animation
		if (power <= 0 && Dead == false) {
			body.simulated = false;
			transform.DOMoveY (transform.position.y + 8, 1.5f).SetEase(Ease.Linear);
			StartCoroutine(Death());
			Dead = true;
		}

		//Get direction
		directionX = lastPosX - transform.position.x;
		directionY = lastPosY - transform.position.y;
		lastPosX = transform.position.x;
		lastPosY = transform.position.y;
		if (Mathf.Abs(directionX) > 0)
			directionY = 0;
		


		if (directionX > 0) {
			_a.Play ("Running left");
			_a.SetBool ("isRunning", true);
		}else if (directionX < 0) {
			_a.Play ("Running right");
			_a.SetBool ("isRunning", true);
		}else if (directionY > 0) {
			_a.Play ("Running front");
			_a.SetBool ("isRunning", true);
		}else if (directionY > 0) {
			_a.Play ("Running back");
			_a.SetBool ("isRunning", true);
		}else if (directionY == 0 && directionX == 0) {
			_a.SetBool ("isRunning", false);
		}


	}
	public void StartNavigation() {
		
		if (target != null) {
			body.constraints = RigidbodyConstraints2D.FreezeAll;
			desk = target.GetComponent<Desk> ();
			desk.StopWork ();
			desk.isComing = true;
			navigate = DOTween.Sequence ().SetId ("navigate");
			navigate.Append (transform.DOMove (new Vector3 (target.transform.position.x, target.transform.position.y, 0), Vector2.Distance(target.transform.position, transform.position)/speed).SetEase (Ease.Linear));
		}
	}










	public void Switch(int id){
		if (id != -1)
			prefferedDeskID = id;
		else{
			randomList.Clear();
			foreach (GameObject desk in GameObject.FindGameObjectsWithTag ("Desk")){
				if (desk.GetComponent<Desk> ().isComing == false) {
					randomList.Add (desk.GetComponent<Desk> ().deskID);
				}
			}
			prefferedDeskID = (int)randomList[Random.Range (0, randomList.Count)];
		}
		if (desk != null) {
			desk.StopWork ();
		}
		Popup();
	}

	public void Popup(){
		Destroy (bubble);
		bubble = (GameObject)Instantiate(Resources.Load(string.Format("{0}{1}", "Bubble", prefferedDeskID.ToString())), new Vector2(transform.position.x, transform.position.y + 0.75f), transform.rotation, transform);
	}






	//Convincing robots
	public void StartConvinceGame(){
		if (target != null) {
			workManager.originalPads = -1;
			GameObject.Find ("Player").GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			workManager.convincingRobot = gameObject;
			GameObject.Find ("GameManager").GetComponent<BoardCreator> ().Generate (convinceTimes);
			StartCoroutine (PadObjects ());
		}
	}
	public void ConvinceSuccess(){
		prefferedSwitchCounter = 30;
		convinceTimes+= 2;
		workManager.padsLeft = 1;
		GameObject.Find ("Player").GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
		prefferedDeskID = target.GetComponent<Desk> ().deskID;
		Destroy (GameObject.Find ("BoardHolder"));
		Destroy (bubble);
		bubble = (GameObject)Instantiate(Resources.Load(string.Format("{0}{1}", "Bubble", prefferedDeskID.ToString())), new Vector2(transform.position.x, transform.position.y + 0.75f), transform.rotation, transform);
		Destroy (bubble, 2f);
	}
	public void ConvinceFail(){
		prefferedSwitchCounter = 30;
		workManager.padsLeft = 1;
		GameObject.Find ("Player").GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
		Destroy (GameObject.Find ("BoardHolder"));
		prefferedDeskID = -1;
		Destroy (bubble);
		bubble = (GameObject)Instantiate(Resources.Load("BubbleX"), new Vector2(transform.position.x, transform.position.y + 0.75f), transform.rotation, transform);
		Destroy (bubble, 2f);
	}
	IEnumerator PadObjects(){
		yield return 0;
		workManager.padsLeft = GameObject.FindGameObjectsWithTag ("Pad").Length;
		workManager.originalPads = workManager.padsLeft;
	}







	public void Animate (int AnimationID){
		if (AnimationID == 1){
			StopAnimating ();
			transform.DOMoveX (transform.position.x + 1, 1f).SetEase (Ease.Linear);
			Switch (-1);
		}
		if (AnimationID == 3){
			StopAnimating ();
			transform.DOMoveX (transform.position.x + 1, 1f).SetEase (Ease.OutBounce);
			Switch (-1);
		}if (AnimationID == 4) {
			StopAnimating ();
			transform.DOMoveX (transform.position.x + 1, 1f).SetEase (Ease.Linear);
			prefferedSwitchCounter = 30;
			Switch (-1);
		}
	}



	void StopAnimating(){
		if (desk.transform.Find("Sprite texture").GetComponent<Animator> () != null) {
			desk.transform.Find("Sprite texture").GetComponent<Animator> ().SetBool ("Animate", false);
		}
	}







	public void LowerConvinceTimes (){
		if (convinceTimes >= 4) {
			convinceTimes -= 2;
		}
		speed += 0.2f;
	}

	public IEnumerator Death()
	{
		workManager.score -= 30;
		int max = Random.Range (9, 15);
		for (int i = 0; i < max; i++)
		{
			Instantiate (explosion, new Vector2 (transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.1f, 0.3f)), transform.rotation);

			yield return new WaitForSeconds(0.15f);
		}
		desk.StopWork ();
		StopAnimating ();
		Destroy (gameObject);

	}
}

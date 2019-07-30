using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Power : MonoBehaviour {

	public Image powerBar;
	public float systemPower = 100f;
	public GameObject Battery;
	public Transform parent;
	public Text score;
	public Text hiScore;

	int scoreI;
	int k;
	WorkManager workManager;
	// Use this for initialization
	void Start () {
		hiScore.text = "" + PlayerPrefs.GetInt("highscore");
		workManager = GameObject.Find ("Work Manager").GetComponent<WorkManager> ();
		powerBar = gameObject.GetComponent<Image> ();	
	}
	
	// Update is called once per frame
	void Update () {
		scoreI = (int)workManager.timer + workManager.score;
		//print (workManager.score + "    Time:" + workManager.timer);
		score.text = "" + scoreI;
		if (systemPower <= 0) {
			Camera.main.cullingMask = new LayerMask();
			StartCoroutine (ReturnToMenu());
		}
		k = Mathf.RoundToInt(systemPower/10);
		switch (k) {
		case 1:
			powerBar.fillAmount = 0.11f;
			break;
		case 2:
			powerBar.fillAmount = 0.21f;
			break;
		case 3:
			powerBar.fillAmount = 0.3f;
			break;
		case 4:
			powerBar.fillAmount = 0.4f;
			break;
		case 5:
			powerBar.fillAmount = 0.5f;
			break;
		case 6:
			powerBar.fillAmount = 0.585f;
			break;
		case 7:
			powerBar.fillAmount = 0.687f;
			break;
		case 8:
			powerBar.fillAmount = 0.777f;
			break;
		case 9:
			powerBar.fillAmount = 0.867f;
			break;
		case 10:
			powerBar.fillAmount = 1f;
			break;
		default:
			break;
		}
		if (systemPower > 100f) {
			systemPower = 100f;
		}
		systemPower -= 0.2f*Time.deltaTime;
	}
	public void TransferPower(){
		if (GameObject.FindGameObjectWithTag ("PowerTransfer") == null) {
			Instantiate (Battery, new Vector3 (transform.position.x, transform.position.y, -8), transform.rotation);
			systemPower -= 10;
		} else { 
			PowerTransfer powerTransfer = GameObject.FindGameObjectWithTag ("PowerTransfer").GetComponent<PowerTransfer> ();
			if (powerTransfer.storedPower < 3) {
				powerTransfer.storedPower += 1;
				systemPower -= 10;
			}
		}
	}
	IEnumerator ReturnToMenu(){
		PlayerPrefs.SetInt ("highscore", scoreI);
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("Main Menu");
	}
}

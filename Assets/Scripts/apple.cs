using UnityEngine;
using System.Collections;

public class apple : Photon.MonoBehaviour {

	public AudioClip appleEat;
	int timeCounter;
	int maxScale;

	// Use this for initialization
	void Start () {
		timeCounter = 0;
		maxScale = setMaxScale ();
	}
	
	// Update is called once per frame
	void Update () {

		if (timeCounter < maxScale) {
			transform.localScale = new Vector3 (timeCounter / 100.0f, timeCounter / 100.0f, 0.0f);
		} else if ( timeCounter == maxScale) {
			transform.rigidbody2D.gravityScale = 0.4f;
		}
		timeCounter++;
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "ground") {
			PhotonView.Destroy (gameObject);
		}
	}

	//  0 ~ 3000 : big apple
	// 3001 ~ 9700 : normal size
	// 9701 ~ 10000 : small apple
	int setMaxScale () {
		 int random = Random.Range ( 0, 10000);
		if (random <= 3000) {
			return 60;
		} else if (3000 < random && random <= 9500) {
			return 40;
		} else {
			return 20;
		}
	}
}

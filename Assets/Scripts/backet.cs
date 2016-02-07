using UnityEngine;
using System.Collections;

public class backet : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "apple_red" || other.tag == "apple_green") {
			audio.PlayOneShot (audio.clip);
			Debug.Log ("get apple !");
			if(photonView.isMine) {
				game.appleCounterUp(other.tag);
			}

			PhotonView.Destroy (other.gameObject);
		}
	}
}

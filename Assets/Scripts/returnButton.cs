using UnityEngine;
using System.Collections;

public class returnButton : MonoBehaviour {

	public GameObject noExistOtherNameRoom;

	// Use this for initialization
	void Start () {
		noExistOtherNameRoom = GameObject.FindWithTag ("noExistOtherNameRoom");
		noExistOtherNameRoom.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnPush() {
		noExistOtherNameRoom.SetActive (false);
		gameObject.SetActive (false);
	}
}

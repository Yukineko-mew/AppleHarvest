using UnityEngine;
using System.Collections;

public class createRoomButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnPush() {
		GameObject networkManager = GameObject.Find ("networkManager");
		networkManager.SendMessage ("OnPushCreateRoomProccess");
	}
}

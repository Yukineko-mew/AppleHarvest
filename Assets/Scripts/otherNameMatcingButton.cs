using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class otherNameMatcingButton : MonoBehaviour {

	private GameObject networkManager;
	public InputField otherNameInputField;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onPush() {
		game.otherName = otherNameInputField.text;

		networkManager = GameObject.Find ("networkManager");
		networkManager.SendMessage ("JoinOtherUserNameRoom");
	}
}

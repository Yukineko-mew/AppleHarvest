using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class enterButton : MonoBehaviour {

	private GameObject networkManager;
	public InputField inputField;

	// Use this for initialization
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnUserNameDecided() {
		game.userName = inputField.text;
		networkManager = GameObject.Find ("networkManager");
		networkManager.SendMessage ("AccessServer");
	}
}

using UnityEngine;
using System.Collections;

public class startButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SceneLoad () {
		// Application.LoadLevel("main");
		fadeManager.Instance.LoadLevel("game",1.0f);
	}
}

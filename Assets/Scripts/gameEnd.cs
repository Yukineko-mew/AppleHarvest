using UnityEngine;
using System.Collections;

public class gameEnd : MonoBehaviour {

	public GameObject resultFrame;

	// Use this for initialization
	void Start () {
		resultFrame = GameObject.Find ("resultFrame");
		resultFrame.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

	}
}

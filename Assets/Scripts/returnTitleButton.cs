using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class returnTitleButton : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public  void OnReturnTitleButton() {
		fadeManager.Instance.LoadLevel("newTitle",1.0f);
	}
}

using UnityEngine;
using System.Collections;

public class title : MonoBehaviour {

//	GameObject gameObject; 
	float fadeTime = 5f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		iTween.MoveTo(gameObject, new Vector3(-0.5f, 8.5f, -1f), fadeTime);
	}
}

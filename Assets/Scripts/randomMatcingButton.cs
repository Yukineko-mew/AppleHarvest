using UnityEngine;
using System.Collections;

public class randomMatcingButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPush() {
		PhotonNetwork.JoinRandomRoom ();
	}
}

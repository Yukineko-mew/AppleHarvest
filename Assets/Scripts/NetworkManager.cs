using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : Photon.MonoBehaviour {

//	public InputField inputField;
//	string userName;
//	public GameObject nameInputField;
//	public GameObject nameInputButton;
/*	
	public GameObject otherNameInputField;
	public GameObject otherNameMatchingButton;

	public GameObject playForFriendButton;
	public GameObject randomMatchingButton;
	public GameObject createRoomButton;

	public GameObject noExistOtherNameRoomReturnButton;
*/	
	public GameObject loadAnimation;
	public GameObject loadImage;
	
	private GameObject instantiatedLoadImage;
	private GameObject instantiatedLoadAnimation;

	void Awake(){
//		nameInputField = GameObject.FindWithTag ("nameInputField");
//		nameInputButton = GameObject.FindWithTag ("nameInputButton");

//		playForFriendButton = GameObject.FindWithTag ("playForFriends");
//		randomMatchingButton = GameObject.FindWithTag ("randomMatcingButton");
//		createRoomButton = GameObject.FindWithTag ("createRoomButton");

//		otherNameInputField = GameObject.FindWithTag ("otherNameInputField");
//		otherNameMatchingButton = GameObject.FindWithTag ("otherNameMatchingButton");



//		noExistOtherNameRoomReturnButton = GameObject.FindWithTag ("returnButton");
	}

	void Start() {
//		nameInputField.SetActive (true);
//		nameInputButton.SetActive (true);

//		otherNameInputField.SetActive (false);
//		otherNameMatchingButton.SetActive (false);

//		playForFriendButton.SetActive (false);
//		randomMatchingButton.SetActive (false);
//		createRoomButton.SetActive (false);
		AccessServer ();
	}

	public void AccessServer() {

		instantiatedLoadImage = Instantiate (loadImage) as GameObject;
		instantiatedLoadAnimation = Instantiate (loadAnimation) as GameObject;

		//マスターサーバーへ接続
		PhotonNetwork.sendRate = 60;
		PhotonNetwork.sendRateOnSerialize = 60;
		PhotonNetwork.ConnectUsingSettings("v0.1");
		Debug.Log ("connecting Master Server");

//		nameInputField.SetActive (false);
//		nameInputButton.SetActive (false);

	}
	
	//ロビー参加成功時のコールバック
	void OnJoinedLobby() {
		//ランダムにルームへ参加
		Debug.Log ("Joined Lobby");

		PhotonNetwork.JoinRandomRoom ();
//		otherNameInputField.SetActive (true);
//		playForFriendButton.SetActive (true);
//		otherNameMatchingButton.SetActive (true);
//		randomMatchingButton.SetActive (true);
//		PhotonNetwork.JoinRandomRoom();
	}
	
	// randomルーム参加失敗時のコールバック
	void OnPhotonRandomJoinFailed() {
		Debug.Log("ルームへの参加に失敗しました");
		//ルームを作る 
		//roomName:部屋名
		//isVisible:ロビーでルームリストにルームを表示
		//isOpen:他のプレイヤーの参加させるか否か
		//maxPlayers:参加出来る最大人数

		// ランダムに入室失敗した場合、ルームを作成
		// ルームオプションの作成
		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.isVisible = true;
		roomOptions.isOpen = true;
		roomOptions.maxPlayers = 2;
		roomOptions.customRoomProperties = new Hashtable (){{"isGamePlay", false} };
		roomOptions.customRoomPropertiesForLobby = new string[] {"isGamePlay"};

		// create random userName(roomName)
		string roomName = "Guest" + UnityEngine.Random.Range(1, 9999);

		// ルームの作成
		PhotonNetwork.CreateRoom ( roomName, roomOptions, null);
		game.isHost = true;
//		PhotonNetwork.CreateRoom("testRoom");
		Debug.Log ("created room.");
	}
	
	//ルーム参加成功時のコールバック
	void OnJoinedRoom() {
		Debug.Log( "[" + PhotonNetwork.room + "]ルームへの参加に成功しました");

//		otherNameInputField.SetActive (false);
//		otherNameMatchingButton.SetActive (false);
//		randomMatchingButton.SetActive (false);
//		playForFriendButton.SetActive (false);

		game.isJoinedRoom = true;

		//プレイヤーをインスタンス化
		Vector3 spawnPosition = new Vector3(0f,-4.5f,-5f); //生成位置
		PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity, 0);
	}

	void OnGUI() {
		//サーバーとの接続状態をGUIへ表示
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString() +  PhotonNetwork.inRoom);
	}
	
	//接続が切断されたときにコール
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("Disconnected from Photon.");

	}

	public void JoinOtherUserNameRoom() {
		/*
		// 既存のRoomを取得.
		RoomInfo [] roomInfo = PhotonNetwork.GetRoomList();
		 // no room
		if (roomInfo == null || roomInfo.Length == 0) {
			// show no room message
//			noExistOtherNameRoomReturnButton.SetActive (true);
		}
		
		// 個々のRoomの名前を表示.
		for (int i = 0; i < roomInfo.Length; i++) {
			 if(game.otherName == roomInfo[i].name) {
				 // if otherName room is existed
				PhotonNetwork.JoinRoom(game.otherName);
				break;
			}
		}
		*/
		PhotonNetwork.JoinRoom (game.otherName);
	}

	public void OnPushCreateRoomProccess() {
		//ルームを作る 
		//roomName:部屋名
		//isVisible:ロビーでルームリストにルームを表示
		//isOpen:他のプレイヤーの参加させるか否か
		//maxPlayers:参加出来る最大人数
		
		// ランダムに入室失敗した場合、ルームを作成
		// ルームオプションの作成
		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.isVisible = false;
		roomOptions.isOpen = true;
		roomOptions.maxPlayers = 2;
		roomOptions.customRoomProperties = new Hashtable (){{"isGamePlay", false} };
		roomOptions.customRoomPropertiesForLobby = new string[] {"isGamePlay"};
		
		// create random userName(roomName)

		// ルームの作成
		PhotonNetwork.CreateRoom ( game.userName, roomOptions, null);
		game.isHost = true;

//		otherNameInputField.SetActive (false);
//		otherNameMatchingButton.SetActive (false);
//		createRoomButton.SetActive (false);
	}

	public void OnPushPlayForFriendButton() {
//		playForFriendButton.SetActive(false);
//		randomMatchingButton.SetActive(false);
//
//		otherNameInputField.SetActive (true);
//		otherNameMatchingButton.SetActive (true);
//		createRoomButton.SetActive (true);


	}

	public void DisconnectNetwork() {
		PhotonNetwork.Disconnect ();
	}

	public void DestroyCountdown() {
		Destroy (instantiatedLoadImage);
		Destroy (instantiatedLoadAnimation);
	}
}

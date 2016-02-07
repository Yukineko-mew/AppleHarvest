using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class game : Photon.MonoBehaviour {

	public static bool isGamePlay;
	public static bool isGameEnd;
	public static bool isCountdowned;
	public static bool isHost;
	public static bool isJoinedRoom;

	public static string userName;
	public static string otherName;
	public Text redScore;
	public Text greenScore;
	public Text timer;
	private int maxPlayer;
	private float timerLimit;
	private int bearAppleInterval;

	public GameObject networkManager;
	public GameObject appleGreenObject;
	public GameObject appleRedObject;
	public Image imageCountdown;

	// when use game end
	public GameObject myScore;
	public GameObject yourScore;
	public Sprite winImage;
	public Sprite loseImage;
	public Sprite drawImage;
	public GameObject winOrLose;
	public GameObject returnTitleButton;
	public GameObject gameend1;
	public GameObject gameend2;
	public GameObject endline;
	public GameObject networkDisconnect;
	
	public Sprite count1;
	public Sprite count2;
	public Sprite count3;
	public Sprite count4;

	public int counter;
	public static int redAppleCounter;
	public static int greenAppleCounter;
	public static int otherRedAppleCounter;
	public static int otherGreenAppleCounter;

	AudioSource [] audios;
	
	// Use this for initialization
	void Start () {

		audios = gameObject.GetComponents<AudioSource> ();
		timerLimit = 60;
		bearAppleInterval = 60;
		counter = 0;
		redAppleCounter = 0;
		greenAppleCounter = 0;
		otherRedAppleCounter = 0;
		otherGreenAppleCounter = 0;
		redScore.text = "x0";
		greenScore.text = "x0";

		isGamePlay = false;
		isGameEnd = false;
		isCountdowned = false;
		isHost = false;
		isJoinedRoom = false;
		
		userName = "hoge";
		maxPlayer = 2;

		winOrLose.SetActive(false);
		returnTitleButton.SetActive(false);
		gameend1.SetActive(false);
		gameend2.SetActive(false);
		endline.SetActive(false);
		networkDisconnect.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isGamePlay) {
			// ------ create apple begin ------
			if(isHost) {
				float x = Random.Range (-2.0f, 2.0f);
				float y = Random.Range (-1.0f, 2.0f);
				float z = 0.0f;

				string appleObjectName;
				float rand = Random.Range (0.0f, 3.0f);
				if (rand <= 1.0f) {
					appleObjectName = "apple_green";
				} else {
					appleObjectName = "apple_red";
				}

				//オブジェクトを生産
				// default : 60
				// last 15 minutes: 30
				// last 5 minutes : 10
				if(timerLimit <=  15) { bearAppleInterval = 30; }
				if(timerLimit <= 5){ bearAppleInterval = 10; }
				if (counter % bearAppleInterval == 0) {
					PhotonNetwork.Instantiate (appleObjectName, new Vector3 (x, y, z), Quaternion.identity , 0);
				}
			}

			// -----  create apple end -----

//			redScore.text = "x" + redAppleCounter.ToString ();
//			greenScore.text = "x" + greenAppleCounter.ToString();

			counter++;

			// ----- countdown timer begin -----
			timerLimit -= Time.deltaTime;
			if(timerLimit <= 0) {
				isGamePlay = false;
				isGameEnd = true;
			}
			timer.text = ((int)timerLimit).ToString ();

			if( PhotonNetwork.room.playerCount == 1 ) { 
				disconnectNetworkProcessing ();
			}

			// ----- countdown timer end -----
		} else if (isGameEnd) {
			StartCoroutine(GameEndStatement());

		} else if(isCountdowned){
			//  game start countdown start
			StartCoroutine(CountdownCoroutine());
			isCountdowned = false;
		}

		if( isJoinedRoom ) {
			if(PhotonNetwork.room.playerCount == maxPlayer) {
				isCountdowned = true;
				networkManager = GameObject.Find("networkManager");
				networkManager.SendMessage("DestroyCountdown");
				maxPlayer++;
			}
		}
	}

	IEnumerator  GameEndStatement() {
		isGameEnd = false;

		yield return new WaitForSeconds (3.0f);

		audios[1].Play();

		yield return new WaitForSeconds (2.0f);

		GameObject networkManager = GameObject.Find ("networkManager");
		game.isJoinedRoom = false;
		networkManager.SendMessage("DisconnectNetwork");

		// show result
		myScore.SetActive(true);
		//			myScore.SendMessage("changeViewable");
		int myScoreNum = redAppleCounter - greenAppleCounter;
		myScore.GetComponent<Text>().text = "あなた:" + myScoreNum;
		
		yourScore.SetActive(true);
		//			yourScore.SendMessage("changeViewable");
		int yourScoreNum = otherRedAppleCounter - otherGreenAppleCounter;
		yourScore.GetComponent<Text>().text = "あいて:" + yourScoreNum;
		
		winOrLose.SetActive(true);
		//			winOrLose.SendMessage("changeViewable");
		if(yourScoreNum < myScoreNum) {
			// win
			winOrLose.GetComponent<Image>().sprite = winImage;
		} else if(yourScoreNum == myScoreNum) {
			// draw
			winOrLose.GetComponent<Image>().sprite = drawImage;
		} else {
			// lose
			winOrLose.GetComponent<Image>().sprite = loseImage;
		}
		
		//			returnTitleButton.SendMessage("changeViewable");
		returnTitleButton.SetActive(true);
		
		gameend1.SetActive(true);
		gameend2.SetActive(true);
		endline.SetActive(true);

	}

	IEnumerator CountdownCoroutine()
	{
		imageCountdown.gameObject.SetActive(true);

		imageCountdown.sprite = count3;
		audios[0].Play ();
		yield return new WaitForSeconds(1.0f);
		
		imageCountdown.sprite = count2;
		audios[0].Play ();
		yield return new WaitForSeconds(1.0f);
		
		imageCountdown.sprite = count1;
		audios[0].Play ();
		yield return new WaitForSeconds(1.0f);
		
		imageCountdown.sprite = count4;
		audios[1].Play ();
		yield return new WaitForSeconds(1.0f);
		
		imageCountdown.gameObject.SetActive(false);
		
		// game start !
		isGamePlay = true;
	}
	
	public void disconnectNetworkProcessing() {
		GameObject networkManager = GameObject.Find ("networkManager");
		game.isJoinedRoom = false;
		networkManager.SendMessage("DisconnectNetwork");
		
		networkDisconnect.SetActive (true);
		returnTitleButton.SetActive(true);
		
		isGamePlay = false;
	}

	private string scoreCalc() {
		int score = 0;

		return score.ToString ();
	}

	public static void appleCounterUp(string apple) {
		if (apple == "apple_red") {
			redAppleCounter++;
		} else {
			greenAppleCounter++;
		}
	}
}

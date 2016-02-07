using UnityEngine;

public class SynchronizedDataSet : Photon.MonoBehaviour {
	//受信データ
	private Vector3 receivePosition = Vector3.zero;
	private Vector3 receiveLocalScale = Vector3.zero;
	private int otherRed;
	private int otherGreen;
//	private Quaternion receiveRotation = Quaternion.identity;
//	private Vector2 receiveVelocity = Vector2.zero;
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			//データの送信
			stream.SendNext(transform.position);
			stream.SendNext(transform.localScale);
			stream.SendNext(game.redAppleCounter);
			stream.SendNext(game.greenAppleCounter);
//			stream.SendNext(transform.rotation);
//			stream.SendNext(rigidbody2D.velocity);
		} else {
			//データの受信（変数へ格納）
			receivePosition = (Vector3)stream.ReceiveNext();
			receiveLocalScale = (Vector3)stream.ReceiveNext();
			otherRed = (int)stream.ReceiveNext();
			otherGreen = (int)stream.ReceiveNext();
//			Debug.Log("red:" + red + ", green:" + green );
//			receiveRotation = (Quaternion)stream.ReceiveNext();
///			receiveVelocity = (Vector2)stream.ReceiveNext();
		}
	}

	void Update() {
		//自分以外のプレイヤーの補正
		if(!photonView.isMine){
			transform.position = receivePosition; // = Vector3.Lerp(transform.position, receivePosition, Time.deltaTime * 10);
			transform.localScale = receiveLocalScale;

//			transform.rotation = Quaternion.Lerp(transform.rotation, receiveRotation, Time.deltaTime * 10);
//			rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity, receiveVelocity, Time.deltaTime * 10);
		}
		game.otherRedAppleCounter = otherRed;
		game.otherGreenAppleCounter = otherGreen;
	}
}
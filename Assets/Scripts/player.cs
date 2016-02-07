using UnityEngine;
using System.Collections;

public class player : Photon.MonoBehaviour {

	bool isRight  = true;
	public LayerMask groundLayer; //地面のレイヤー
	bool isGrounded;
	float speed = 4.0f;

	// Use this for initialization
	void Start () {
		transform.rigidbody2D.gravityScale = 15.0f;

		if(!photonView.isMine) {
			SpriteRenderer renderer = this.GetComponent<SpriteRenderer> ();
			renderer.color = new Color (0.25f, 0.25f, 0.25f, 1.0f);
			SpriteRenderer childRenderer = transform.FindChild ("basket").gameObject.GetComponent<SpriteRenderer>();
			childRenderer.color = new Color (0.25f, 0.25f, 0.25f, 1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			// 右 plus : 左 minus
			float x = Input.GetAxisRaw ("Horizontal");

			// 移動する向きを求める
			Vector2 direction = new Vector2 (x, 0.0f).normalized;

			// 移動する向きとスピードを代入する
			GetComponent<Rigidbody2D> ().velocity = direction * speed;

			if ((x > 0 && !isRight) || (x < 0 && isRight)) {
				//右を向いているかどうかを、入力方向をみて決める
				isRight = (x > 0);
				//localScale.xを、右を向いているかどうかで更新する
				transform.localScale = new Vector3 ((isRight ? 0.2f : -0.2f), 0.2f, 1.0f);
			}

			//ユニティちゃん中央から足元にかけて、接地判定用のラインを引く
			isGrounded = Physics2D.Linecast (
			transform.position + transform.up * 0.3f,
			transform.position - transform.up * 0.1f,
			groundLayer); //Linecastが判定するレイヤー

			//isGrounded=true且つJumpボタンを押した時Jumpメソッド実行
			if (isGrounded && Input.GetButtonDown ("Jump")) {
				jump ();
			}
		}
	}

	void jump() {
		rigidbody2D.AddForce(Vector2.up * 3000);
		isGrounded = false;
	}
}

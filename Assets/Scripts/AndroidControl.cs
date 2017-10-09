using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnitySampleAssets.CrossPlatformInput;

public class AndroidControl : MonoBehaviour {
	public float speed;
	public float turnSpeed = 10;
	Rigidbody playerRigidbody;
	//MyPlayerManager playerManager;

	int floorMask;
	float camRayLength = 25;
	Vector3 movement;
	Vector3 turning;

	public float spdUpTimer;
	bool pwrUpSpeed = false;
	public int pwrUpSpeedBoost = 0;

	void Awake(){

		floorMask = LayerMask.GetMask ("Floor");
		playerRigidbody = GetComponent <Rigidbody> ();

	}
		
	

	void FixedUpdate () {

		//Snap to full speed instead of accelarating
		float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		float v = CrossPlatformInputManager.GetAxis ("Vertical");
		float tH = CrossPlatformInputManager.GetAxis ("TurnAxisHoriz");
		float tV = CrossPlatformInputManager.GetAxis ("TurnAxisVert");

	
		if (pwrUpSpeed) {
			//Debug.Log ("SpeedUp Timer at: " + spdUpTimer);
			spdUpTimer += Time.deltaTime;
			if (spdUpTimer > 20f) {

				pwrUpSpeed = false;
				pwrUpSpeedBoost = 0;
				spdUpTimer = 0;
			}
		}

		Move (h, v);
		Turning (tH, tV);
	}

	void Move(float h, float v){
		Debug.Log ("Should be moving");
		movement.Set (h, 0f, v);
		movement = movement.normalized * (speed + pwrUpSpeedBoost) * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning(float tH, float tV){

		Vector3 targetDir = CrossPlatformInputManager.mousePosition - transform.position;
		float angle = Vector3.Angle(targetDir, transform.forward);

	}

	public void PwrUp_Speed(){
		//Debug.Log ("Should Get Speed"); 
		pwrUpSpeed = true;
		pwrUpSpeedBoost = 10;
		//Debug.Log(" Current Speed Now At: " + movement);
	}
		
}

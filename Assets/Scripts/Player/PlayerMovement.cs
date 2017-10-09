﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour {

	public float speed;
	public float xtilt, ytilt;


	Vector3 movement;
	//Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100;


	void Awake(){

		floorMask = LayerMask.GetMask ("Floor");
		playerRigidbody = GetComponent <Rigidbody> ();
	}

	void FixedUpdate(){

		 

		//Snap to full speed instead of accelarating
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");


		Move (h, v);
		Turning ();
	}
 
	void Move(float h, float v){
		
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning(){

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);

			//Quaternion tiltRotation = Quaternion.Euler (playerToMouse.normalized);
			//playerRigidbody.MoveRotation (tiltRotation);

		}
	}

	

}

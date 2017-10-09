using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

	public int planetNumber;
	GameObject player, collidingObject;
//	void Start(){
//		player = GameObject.FindGameObjectWithTag ("Player");
//	}

	void OnTriggerEnter(Collider other) {
		

		collidingObject = other.gameObject;

		if (collidingObject.tag == "Player") {
			Debug.Log ("Detected Collision");

			switch(planetNumber){

			case 1:
				SceneManager.LoadScene ("Level 1");
				break;
			case 2:
				SceneManager.LoadScene ("Level 2");
				break;
			case 3:
				SceneManager.LoadScene ("Level 3");
				break;
			}
		}
	}
}

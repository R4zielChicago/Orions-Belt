using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TzarBombHit : MonoBehaviour {

	public int attackDamage = 30;
	public float maxSpeed = 1.5f;
	public GameObject explosion;
	float increase = 1f;
	GameObject player;
	GameObject bullet;
	MyPlayerManager playerHealth;
	AudioSource enemyAudio;


	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {

			playerHealth = player.GetComponent<MyPlayerManager> ();
			bullet = this.gameObject;
		}
	}

	void Update(){

		// Speeds Up The Rocket As It Flies

		// Maybe make it glow as it flies as well.
		increase += 0.2f;
		if (increase > maxSpeed){
			increase = maxSpeed;
		}
		GetComponent <Rigidbody>().AddForce (transform.forward   * increase ,  ForceMode.VelocityChange);
	}


	void OnTriggerEnter(Collider other) {
			GameObject hitObject = other.gameObject;
			player = GameObject.FindGameObjectWithTag ("Player");


		if (other.tag == "Boundary"){
			return;
		}

		/*Destroy (gameObject);*/
			if(player != null){

			if (other.gameObject == GameObject.FindGameObjectWithTag ("Player")) {
				playerHealth.TakeDamage (attackDamage);
				Destroy (bullet); 
				//Debug.Log ("Take Damage", player);
			}
		}


		/*if (player == null) {
			GameObject clone;
			clone = Instantiate (explosion, transform.position, transform.rotation);
			//enemyAudio.Play ();
			Destroy (hitObject);
			DestroyObject (clone, 2);

		}*/
	}
}


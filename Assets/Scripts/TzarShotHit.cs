using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TzarShotHit : MonoBehaviour {


	public int attackDamage = 30;
	public GameObject explosion;

	GameObject player;
	GameObject bullet;
	PlayerHealth playerHealth;
	AudioSource enemyAudio;


	void Awake(){

		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		bullet = this.gameObject;
	}

	void OnTriggerEnter(Collider other) {
		GameObject hitObject = other.gameObject;
		if (other.tag == "Boundary"){
			return;
		}

		/*Destroy (gameObject);*/
		if(playerHealth.currentHealth > 0f){

			if (other.gameObject == GameObject.FindGameObjectWithTag ("Player")) {
				playerHealth.TakeDamage (attackDamage);
				Destroy (bullet); 

			}
		}


		if (playerHealth.currentHealth <= 0f) {
			GameObject clone;
			clone = Instantiate (explosion, transform.position, transform.rotation);
			Destroy (hitObject);
			DestroyObject (clone, 2);
			enemyAudio.Play ();
		}
	}
}

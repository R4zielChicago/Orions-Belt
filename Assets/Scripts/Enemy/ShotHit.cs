using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotHit : MonoBehaviour {

	public int attackDamage = 10;
	public GameObject explosion;

	GameObject player;
	GameObject asteroid;
	GameObject bullet;
	MyPlayerManager playerHealth;
	EnemyRemote asteroidHealth;
	AudioSource playerExplosionAudio;


	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {

			playerHealth = player.GetComponent<MyPlayerManager> ();
			bullet = this.gameObject;
		}
	}

	void OnTriggerEnter(Collider other) {
		GameObject hitObject = other.gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");

		switch (hitObject.tag) {
		case "Bullet":
			// If Bullet hits another bullet
			//Debug.Log ("HIT BY BULLET ");
			// Destroy self on bullet hit
			Destroy (gameObject);
			break;

		case "PlayerSupport":
			// If Bullet hits player's support
			//Debug.Log (gameObject + "shot  Support Ship" + hitObject);
			// Damage Support Ship
			hitObject.GetComponent <PlayerSupportManager> ().TakeDamage (attackDamage);
			// Destroy Bullet
			Destroy (gameObject);
			break;

		case "Player": 
			// If Bullet hits player
			playerHealth.TakeDamage (attackDamage);
			Destroy (bullet); 
			Debug.Log ("Take Damage", player);
			break;

		default:
			// ignore
			break;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	public EnemyRemote enemyAttackDamage;
	public ShotHit shotHit;
	public PlayerHealth	playerHealth;
	public EnemyRemote enemyHealth;
	public GameObject explosion;
	AudioSource enemyAudio;
	AudioSource playerExplosionAudio;
	GameOverManager asteroidHit;

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Boundary"){
				return;
		}
			
		/*Destroy (gameObject);*/
		if(other.tag == "Player"){
			
			/*GameObject clone;*/
			/*clone = Instantiate (explosion, transform.position, transform.rotation);*/
			Destroy (other.gameObject);
			playerHealth.currentHealth = 0;
			playerHealth.Death ();
			Destroy (other.gameObject);
			/*DestroyObject (clone, 2);*/
			/*	playerExplosionAudio.Play ();


			/*	playerHealth.Death ();
			asteroidHit.anim.SetTrigger ("GameOver");
			asteroidHit.restartTimer += Time.deltaTime;*/
		}
		if (other.tag == "Enemy") {
			Destroy (other.gameObject);


			enemyHealth.currentHealth = 0;
			enemyHealth.Death ();
		}

		if(other.tag == "Bullet"){

				enemyHealth.currentHealth -= shotHit.attackDamage;

		}
	/*	GameObject clone;
		clone = Instantiate (explosion, transform.position, transform.rotation);
		Destroy(other.gameObject);
		Destroy (gameObject); 
		DestroyObject (clone, 2);
		/*enemyAudio.Play ();*/
	}
}

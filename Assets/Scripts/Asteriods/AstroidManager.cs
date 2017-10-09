using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidManager : MonoBehaviour {
	public float speed;
	public float tumble;
	public int scoreValue = 20;
	public int startingHealth = 100;
	public int currentHealth;
	public GameObject explosion;


	ParticleSystem hitParticles;



	bool isDead;

	GameObject player; 
	GameObject collidingObject;
	EnemyRemote enemyAttackDamage;
	ShotHit shotHit;
	MyPlayerManager	playerHealth;


	AudioSource enemyAudio;
	AudioSource playerExplosionAudio;
	GameOverManager asteroidHit;

	void Awake() {
		//explosion = GetComponent<GameObject> ();
		enemyAudio = GetComponent<AudioSource> ();
		hitParticles = GetComponentInChildren<ParticleSystem> ();


		currentHealth = startingHealth;
	}

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");

		if (player != null) {

			transform.LookAt (player.transform.position);
			GetComponent<Rigidbody> ().velocity += transform.forward * speed;
			GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitSphere * tumble;
		}

	}
	public void playerTakeDamage (int amount, Vector3 damageArea){
		

		if (isDead)
			return;


		enemyAudio.Play ();

		currentHealth -= amount;
		//currentHealth -= shotHit.attackDamage;


		hitParticles.transform.position = damageArea;
		hitParticles.Play ();

		if (currentHealth <= 0) {
			//Debug.Log ("Astroid Should Be Dead");
			Death (gameObject);
		}
	}


	public void enemyTakeDamage (int amount){
		
		//Debug.Log ("Should Take Damage", gameObject);

		if (isDead)
			return;

		enemyAudio.Play ();

		currentHealth -= amount;

		if (currentHealth <= 0) {
			//Debug.Log ("Astroid Should Be Dead");
			Death (gameObject);
		}
		currentHealth -= amount;
	}


	void OnTriggerEnter(Collider other) {
		// Get Reference of what hit this object.
		collidingObject = other.gameObject;

		// check of object is not destroyed
		//if (collidingObject != null) {
		// Handles Collision Interactions. 
		switch (collidingObject.tag) {
			case "Bullet":
				//Debug.Log ("HIT BY BULLET ");

				// Destroy Bullet
				Destroy(collidingObject);

				// Take damage from bullet
				enemyTakeDamage(10);
				break;

			case "Enemy":
//				Debug.Log("HIT BY ENEMY SHIP");

				if (collidingObject.name == "EnemyCorvette(Clone)") {
					Death(gameObject);

				} else {

					Death(collidingObject);

				}
				break;

			case "Player":
				//Debug.Log ("Asteroid Hit Player");
				MyPlayerManager playerManager = collidingObject.GetComponent<MyPlayerManager>();
				/*GameObject clone;*/
				/*clone = Instantiate (explosion, transform.position, transform.rotation);*/
				if (collidingObject.GetComponent<MyPlayerManager>().pwrUpShield) {
					Death(gameObject);

				}
				else
				{

					Death(collidingObject);
					playerManager.currentHealth = 0;
				}
	
			break;

		default:
			// ignore
			break;
		}
	//}

		/*	GameObject clone;
		clone = Instantiate (explosion, transform.position, transform.rotation);
		Destroy(other.gameObject);
		Destroy (gameObject); 
		DestroyObject (clone, 2);
		/*enemyAudio.Play ();*/
	}
	public void Death(GameObject objectToDie)
	{
		isDead = true;
		GameObject clone;
		clone = Instantiate (explosion, objectToDie.transform.position, objectToDie.transform.rotation);
		Destroy (objectToDie); 
		ScoreManager.score += scoreValue;
		DestroyObject (clone, 2f);
	}

}

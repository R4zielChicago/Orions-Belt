using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;
using UnityEngine.Serialization;

public class PlayerSupportManager : MonoBehaviour {

	// DECLARE VARIABLES
	public int startingHealth = 100;
	public int currentHealth ;
	public float speed;
	public AudioClip deathClip;
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
	public bool playerIsDead = false;
	public int damagePerShot = 10;
	public float timeBetweenBullets = 0.35f;
	public float timeBetweenAttacks = 0.5f;
	public float AllyLeashRange = 50f;
	public float attackRadius = 20;

	Ray camRay;
	RaycastHit floorHit;
	Vector3 playerToMouse, movement;
	Quaternion newRotation;
	public bool gunsLoaded;
	public GameObject target;


	///Navigation Variables
	NavMeshAgent nav;
	PlayerSupportManager supportScript;
	Transform playerTransform;

	float h, v;
	bool isDead;

	GameObject player;
	public GameObject explosion;
	AudioSource supportDamageAudio;


	bool damaged;

	//Animator anim;

	void Awake(){
		
		player  = GameObject.FindGameObjectWithTag("Player");
		nav = GetComponent<NavMeshAgent> ();
		supportScript = GetComponent<PlayerSupportManager> ();
		supportDamageAudio = GetComponent<AudioSource>();
		/*playerShooting = GetComponentInChildren<PlayerShooting>();*/
		currentHealth = startingHealth;

	}

	void FixedUpdate(){
		AttackEnemies (attackRadius);
	}

	void Update() {
		if (currentHealth > 0) {
			StartCoroutine (FollowTarget(100, player.GetComponent<Transform>()));

		} else {
			nav.enabled = false;
		}

		if (damaged) {

			damageImage.color = flashColor;
		}
		else{

//			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}
		
	public void TakeDamage (int amount)
	{
		//Debug.Log(" Support Health Now At: " + currentHealth);
		damaged = true;
		currentHealth -= amount;
		supportDamageAudio.Play();
		/*Debug.Log ("Shot By: ", shooter);*/

		if (currentHealth <= 0 && playerIsDead == false) {

			GameObject clone;

			clone = Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation);
			DestroyObject (clone, 2);
			Destroy(gameObject);
			Death();
		}
	}

	void Death() {
		playerIsDead = true;

	}	

	// A SCRIPT TO FOLLOW TARGET EFFECTIVELY MAYBE
	     private IEnumerator FollowTarget(float AllyLeashRange, Transform target) {
		// Support Script to follow player
         Vector3 previousTargetPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity);
			
		while (Vector3.SqrMagnitude(transform.position - target.position) > AllyLeashRange) {
             // did target move more than at least a minimum amount since last destination set?
             if (Vector3.SqrMagnitude (previousTargetPosition - target.position) > 1f) {
                 nav.SetDestination (target.position);
                 previousTargetPosition = target.position;
             }
             yield return new WaitForSeconds (0.1f);
         }
         yield return null;
     }

	void AttackEnemies(float radius){


		// Wait until guns are loaded before checking for new enemies. 

		if (gunsLoaded == false) {

			// Create an array of objects within the radius of support ship
			Collider[] hitColliders = Physics.OverlapSphere (gameObject.transform.position, radius);

			// Cycle through array to see which objects are enemies, break while loop if an enemy 
			// is found within range. 
			int i = 0;
			while (i < hitColliders.Length) {

				if (hitColliders [i].gameObject.tag == "Enemy") {

					// Set target GameObject
					target = hitColliders [i].gameObject;

					// Make Support ship face the enemy.
					transform.LookAt (target.transform.position);

					// Set gunsLoaded boolean to true, at this point "Support Shooting's update method takes over. 
					// After shooting,  support shooting script sets gunsloaded to false to repeat the cycle.
					gunsLoaded = true;
					// Debug.Log (hitColliders [i].gameObject);
					break;
				}
				i++;
			}
		}


	}

}
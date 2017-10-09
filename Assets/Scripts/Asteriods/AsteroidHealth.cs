using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.UIElements.StyleEnums;
using System.Runtime.Serialization;

public class AsteroidHealth : MonoBehaviour {


		public int startingHealth = 300;
		public int currentHealth;
		//public float sinkSpeed = 2.5f;
		public int scoreValue = 20;
		public AudioClip deathClip;
		public GameObject explosion;

		AudioSource enemyAudio;
		ParticleSystem hitParticles;
		MeshCollider meshCollider;
		bool isDead;

		void Awake() {


			//explosion = GetComponent<GameObject> ();
			enemyAudio = GetComponent<AudioSource> ();
			hitParticles = GetComponentInChildren<ParticleSystem> ();
			meshCollider = GetComponent<MeshCollider> ();

			currentHealth = startingHealth;
		}


		public void TakeDamage(int amount, Vector3 hitPoint) {

			if (isDead)
				return;

			enemyAudio.Play ();

			currentHealth -= amount;

			hitParticles.transform.position = hitPoint;
			hitParticles.Play ();

			if (currentHealth <= 0) {
				Death ();
			}
		}
		

		void Death()
		{

			isDead = true;

			if(gameObject.tag == "Enemy"){

				meshCollider.isTrigger = true;
			}

			GameObject clone;
			clone = Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject); 
			DestroyObject (clone, 2);


		}



}

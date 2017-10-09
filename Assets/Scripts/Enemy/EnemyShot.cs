using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour {

	GameObject player;
	public Transform playerTransform;
	public float range = 50.0f;
	public float bulletImpulse = 20.0f;
	public float timeBetweenBullets = 0.2f;

	float timer;

	private bool onRange = false;

	public Rigidbody projectile;

	void Start(){

		player = GameObject.FindGameObjectWithTag("Player");
		if (player != null) {
			playerTransform = player.transform;
			float rand = Random.Range (3.0f, 5.0f);
			InvokeRepeating ("Shoot", 5, rand);
		}
	}

	void Update(){

		timer += Time.deltaTime;

		// Check if ship has been destroyed
		if (player != null) {

			onRange = Vector3.Distance (transform.position, playerTransform.position) < range;
		}
	}
		
	void Shoot(){

		if(onRange && timer >= timeBetweenBullets){

			Rigidbody bullet = (Rigidbody)Instantiate (projectile, transform.position + transform.forward , transform.rotation);
			bullet.AddForce (transform.forward * bulletImpulse, ForceMode.Impulse);
		}
	}
}

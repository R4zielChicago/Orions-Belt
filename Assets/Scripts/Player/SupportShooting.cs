using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportShooting : MonoBehaviour {

	public int damagePerShot = 20;
	public float timeBetweenBullets = 0.15f;
	public float gunRange = 30f;
	public Color color1 = Color.blue;
	public Color color2 = Color.blue;

	float timer;
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	ParticleSystem gunParticles;
	LineRenderer gunLine;


	AudioSource gunAudio;
	Light gunLight;
	GameObject asteroid;
	bool LCorRC = true;

	float effectsDisplayTime = 0.2f;

	void Awake() {
		shootableMask = LayerMask.GetMask("Shootable");
		gunParticles = GetComponent<ParticleSystem>();
		gunLine = GetComponent<LineRenderer>();
		gunAudio = GetComponent<AudioSource>();
		gunLight = GetComponent<Light>();

	}

	void Update() {
		timer += Time.deltaTime;

		// Wait till PlayerSupportManager script gives okay before checking to fire. 
		if (GetComponentInParent<PlayerSupportManager> ().gunsLoaded) {
			// Check time between last fire
			if (timer > timeBetweenBullets) {

				// Call method to fire bullets
				Shoot ();
				// Once firing is complete, set gunsLoaded to false;
				GetComponentInParent<PlayerSupportManager> ().gunsLoaded = false;

			}
		}
			if (timer >= timeBetweenBullets * effectsDisplayTime) {
			DisableEffects ();
		}
	}

		public void DisableEffects() {

		gunLine.enabled = false;
		gunLight.enabled = false;
		}

		public void Shoot() {

//		Debug.Log ("Target Is: " + GetComponentInParent<PlayerSupportManager> ().target );
			timer = 0f;
			gunAudio.Play();
			gunLight.enabled = true;
			gunParticles.Stop();
			gunParticles.Play();

			gunLine.enabled = true;
			gunLine.SetPosition(0, transform.position);

			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;

		if (Physics.Raycast (shootRay, out shootHit, gunRange, shootableMask)){
			GameObject hit = shootHit.transform.gameObject;
			if (hit.tag == "Asteroid"){
				AstroidManager enemyHealth = shootHit.collider.GetComponent<AstroidManager> ();
				if (enemyHealth != null) {
					enemyHealth.playerTakeDamage (damagePerShot, shootHit.point);
				}

					//	gunLine.startColor.linear(color1);
				gunLine.SetPosition (1, shootHit.point);	
			}
			if(hit.tag != "Asteroid"){
				EnemyRemote enemyHealth = shootHit.collider.GetComponent<EnemyRemote> ();
				if (enemyHealth != null) {
					enemyHealth.TakeDamage (damagePerShot, shootHit.point);
				}
				gunLine.SetPosition (1, shootHit.point);	
			}
		}
			else{
			gunLine.SetPosition(1, shootRay.origin + shootRay.direction * gunRange);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography.X509Certificates;

public class playerLeft_Cannon : MonoBehaviour {

	public int damagePerShot = 20;
	public float timeBetweenBullets = 0.15f;
	public float range = 100f;

	float timer;
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	ParticleSystem gunParticles;
	LineRenderer gunLine;

	AudioSource gunAudio;
	Light gunLight;
	GameObject asteroid;
	bool LCorRC = false;

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
		if (LCorRC) {
			if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets) {
				Shoot ();
			}

			if (timer >= timeBetweenBullets * effectsDisplayTime) {
				DisableEffects ();
			}
			LCorRC = false;
		} else {
			LCorRC = true;
		}

	}
		public void DisableEffects() {

			gunLine.enabled = false;
			gunLight.enabled = false;


		}

		void Shoot() {

			timer = 0f;
			gunAudio.Play();
			gunLight.enabled = true;
			gunParticles.Stop();
			gunParticles.Play();

			gunLine.enabled = true;
			gunLine.SetPosition(0, transform.position);

			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;

			if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)){
				GameObject hit = shootHit.transform.gameObject;
				if (hit.tag == "Asteroid"){
					AstroidManager enemyHealth = shootHit.collider.GetComponent<AstroidManager> ();
					if (enemyHealth != null) {
						enemyHealth.playerTakeDamage (damagePerShot, shootHit.point);
					}
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
				gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
			}
		}
	}

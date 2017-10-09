using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {


	public int startingHealth = 100;
	public int currentHealth ;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
	public GameObject explosion;
	private GameObject clone;

	AudioSource playerAudio;
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;
	bool isDead;
	bool damaged;

	void Awake() {

		explosion = GetComponent<GameObject>();
		playerAudio = GetComponent<AudioSource>();
		playerMovement = GetComponent<PlayerMovement>();
		playerShooting = GetComponentInChildren<PlayerShooting>();
		currentHealth = startingHealth;
	}

	void Update() {

		if (damaged) {

			damageImage.color = flashColor;
		}
		else{

			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}
	
	public void TakeDamage (int amount/*, GameObject shooter*/)
	{

		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		playerAudio.Play();
		/*Debug.Log ("Shot By: ", shooter);*/
		//Debug.Log(" Health Now At: "+ currentHealth);
		if (currentHealth <= 20f && !isDead) {

			Death();
		}
	}



	public void Death() {
		
		isDead = true;

		playerShooting.DisableEffects();	
		clone = Instantiate (explosion, transform.position, transform.rotation);
		DestroyObject (clone, 2);
		Destroy (gameObject); 


		playerAudio.clip = deathClip;
		playerAudio.Play();

		playerMovement.enabled = false;
		playerShooting.enabled = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MyPlayerManager : MonoBehaviour {

	// DECLARE VARIABLES
	public int startingHealth = 100;
	public int startingShield = 20;
	public int currentHealth;
	public int currentShield;
	public float speed;
	public Slider healthSlider;
	public Slider shieldSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
	public GameObject explosion;
	public GameObject supportShip;
	public GameObject tempShip;


	bool playerIsDead = false;
	GameObject leftGun;
	GameObject rightGun;
	GameObject mainGun;
	Ray camRay;
	RaycastHit floorHit;
	Vector3 playerToMouse, movement;
	Quaternion newRotation;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100;
	float h, v;


	AudioSource playerAudio;
	bool damaged;


	//Power Up Timers
	public float spdUpTimer;
	public float dualShotTimer;
	public float supportTimer;
	public float shieldTimer;

	// Power Up Switches
	bool pwrUpSpeed = false;
	bool pwrUpDual = false;
	bool pwrUpSupport = false;
	public bool pwrUpShield = false;



	// Power Up Variables
	public int pwrUpHealth = 10;
	public int pwrUpSpeedBoost = 0;

	//Animator anim;

	void Awake(){
		
		rightGun = GameObject.Find ("Weapon_Right");
		leftGun = GameObject.Find ("Weapon_Left");
		mainGun = GameObject.Find ("Weapon_Main");
		/*RaycastHit floorHit = new RaycastHit;*/
		floorMask = LayerMask.GetMask ("Floor");
		playerRigidbody = GetComponent <Rigidbody> ();
		playerAudio = GetComponent<AudioSource>();
		/*playerShooting = GetComponentInChildren<PlayerShooting>();*/
		currentShield = startingShield;
		currentHealth = startingHealth;
	}

	void FixedUpdate(){

		//Snap to full speed instead of accelarating
		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");
	
//		Move (h, v);
		Turning ();
	}
	void Update() {
		


		if (pwrUpSpeed) {
			//Debug.Log ("SpeedUp Timer at: " + spdUpTimer);
			spdUpTimer += Time.deltaTime;
			if (spdUpTimer > 20f) {
				
				pwrUpSpeed = false;
				pwrUpSpeedBoost = 0;
				spdUpTimer = 0;
			}
		}

		if (pwrUpDual) {
			//Debug.Log ("DualShot Timer at: " + dualShotTimer);
			dualShotTimer += Time.deltaTime;
			if (dualShotTimer > 20f) {

				pwrUpDual = false;
				dualShotTimer = 0;

				rightGun.GetComponent<LineRenderer> ().enabled = false;
				leftGun.GetComponent<LineRenderer> ().enabled = false;
				rightGun.GetComponent<PlayerShooting> ().enabled = false;
				leftGun.GetComponent<playerLeft_Cannon> ().enabled = false;
				mainGun.GetComponent<PlayerShooting> ().enabled = true;

			}
		}

		if (pwrUpSupport) {
			//Debug.Log ("SupportShot Timer at: " + supportTimer);
			supportTimer += Time.deltaTime;
			if (supportTimer > 20f) {

				pwrUpSupport = false;
				supportTimer = 0;
				GameObject clone;
				clone = (GameObject)Instantiate (explosion, tempShip.transform.position, tempShip.transform.rotation);
				DestroyObject (clone, 2);
				DestroyObject(tempShip);

			}
		}
		if (pwrUpShield) {
			//Debug.Log ("Shield Timer at: " + supportTimer);
			shieldTimer += Time.deltaTime;
			if (shieldTimer > 20f) {
				pwrUpShield = false;
				shieldTimer = 0;
				transform.GetChild(1).gameObject.SetActive(false);
			}
		}
			
		if (damaged) {

			damageImage.color = flashColor;
		}
		else{

			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage (int amount)
	{
		//Debug.Log(" Health Now At: " + currentHealth);
		damaged = true;

		currentShield -= amount;
		shieldSlider.value = currentShield;
		if (currentShield <= 0)
		{
			currentHealth -= amount;
			healthSlider.value = currentHealth;
			playerAudio.Play();
		}
		/*Debug.Log ("Shot By: ", shooter);*/

		if (currentHealth <= 0 && playerIsDead == false) {

			GameObject clone;

			clone = Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation);
			DestroyObject (clone, 2);
			Destroy(gameObject);
			Death();
		}
	}
	//PC Controls	
//	void Move(float h, float v){
//		movement.Set (h, 0f, v);
//		movement = movement.normalized * (speed + pwrUpSpeedBoost) * Time.deltaTime;
//		playerRigidbody.MovePosition (transform.position + movement);
//	}

	void Turning(){
		camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		/// RAY FLOOR HIT NEVER INITALIZED TO ANYTHING??
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
	
		}
	}

	void Death() {
			//Debug.Log ("Should be Dead");
		playerIsDead = true;
	}


	/// <summary>
	///  POWER UP EFFECTS BELOW THIS LINE
	/// </summary>
	/// <param name="regenOnTouch">Regen on touch.</param>
	public void PwrUp_Health(){
		//Debug.Log ("Should Get Health"); 
		currentHealth += pwrUpHealth;
		healthSlider.value = currentHealth;
		//Debug.Log(" Health Now At: " + currentHealth);
	}

	public void PwrUp_Speed(){
		//Debug.Log ("Should Get Speed"); 
		pwrUpSpeed = true;
		pwrUpSpeedBoost = 10;
		//Debug.Log(" Current Speed Now At: " + movement);
	}

	public void PwrUp_Dual(){
		
		pwrUpDual = true;
		leftGun.GetComponent<playerLeft_Cannon> ().enabled = true;
		rightGun.GetComponent<PlayerShooting> ().enabled= true;
		mainGun.GetComponent<PlayerShooting> ().enabled = false;

	}

	public void PwrUp_Support(){
		if (!pwrUpSupport) {
			//Debug.Log ("Should Spawn Support");
			pwrUpSupport = true;
			tempShip = (GameObject)Instantiate (supportShip, gameObject.transform.position, gameObject.transform.rotation);
		}
	}

	public void PwrUp_Shield()
	{
		if (!pwrUpShield)
		{
			//Debug.Log ("Should Spawn Shield");
			pwrUpShield = true;
			transform.GetChild(1).gameObject.SetActive(true);
		}
	}

}

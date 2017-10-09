using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.Experimental.UIElements.StyleEnums;
using System.Runtime.Serialization;

public class EnemyRemote : MonoBehaviour {
	GameObject player;
	GameObject support;
	Transform playerTransform;
	EnemyRemote myScript;
	NavMeshAgent nav;
	GameObject closeHit;
	MyPlayerManager playerScript;
	PlayerSupportManager supportScript;
	public int currentHealth;

	bool playerInRange;
	bool supportInRange;
	float timer;Vector3 playerRotation;

	public int scoreValue = 20;
	public int startingHealth = 100;


	public int damagePerShot = 10;
	public float timeBetweenBullets = 0.35f;
	public float timeBetweenAttacks = 0.5f;
	public float range = 50f;
	public GameObject explosion;
	public GameObject playerExplosion;

	public AudioClip deathClip;
	AudioSource enemyAudio;
	ParticleSystem hitParticles;
	MeshCollider meshCollider;
	bool isDead;


	// On Awake
	void Awake(){
		/*GetComponent<Transform> ().Rotate (0, 30f, 0);*/
		player  = GameObject.FindGameObjectWithTag("Player");
		support = GameObject.FindGameObjectWithTag ("PlayerSupport");

		if (support != null){
			supportScript = support.gameObject.GetComponent<PlayerSupportManager> ();
		}
		if (player != null) {
			playerScript = player.GetComponent<MyPlayerManager> ();
		}

			myScript = GetComponent <EnemyRemote> ();
			playerTransform = player.transform;
			nav = GetComponent<NavMeshAgent> ();
			enemyAudio = GetComponent<AudioSource> ();
			hitParticles = GetComponentInChildren<ParticleSystem> ();
			meshCollider = GetComponent<MeshCollider> ();
			currentHealth = startingHealth;

		/*gunAudio = GetComponent<AudioSource>();*/
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{
		// Attack 
		timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks && myScript.currentHealth > 0) {
			if (playerInRange) {
				Attack ();
			}

			if (supportInRange) {
				AttackSupport ();
			}
		}

		float angle = 30f;

		if (GameObject.Find ("Player")) {

		/*	if (Vector3.Angle (playerTransform.transform.forward, transform.position - playerTransform.transform.position) > angle) {
				//Debug.Log ("I See You", gameObject);
				transform.position += Vector3.right * Time.deltaTime;
			} */
			if (myScript.currentHealth > 0 && playerTransform != null) {
				nav.SetDestination (playerTransform.position);
				/*nav.updateRotation = false;*/
			} else {
				nav.enabled = false;
			}
		}		
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

	public void Death()
	{

		isDead = true;

		if(gameObject.tag == "Enemy"){

			meshCollider.isTrigger = true;
		}

		GameObject clone;
		clone = Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject); 
		ScoreManager.score += scoreValue;
		DestroyObject (clone, 2f);

	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log ( "Ran Into" + other.gameObject.tag);
		if (other.gameObject == player) {
			playerInRange = true;
			}

		if (other.gameObject.tag == "PlayerSupport"){
			supportInRange = true;
		}
	}


	void OnTriggerExit(Collider other)
	{

		if(other.gameObject == player){

			playerInRange = false;
		}

		if (other.gameObject == support){
			supportInRange = false;
		}
	}

	void Attack()
	{
		timer = 0f;
		if (playerScript.pwrUpShield)
		{
			if (myScript.currentHealth > 1f)
			{
				myScript.TakeDamage(damagePerShot, GetComponent<Transform>().position);
			}else if (myScript.currentHealth <= 1f)
			{

				GameObject clone;
				clone = Instantiate(playerExplosion, transform.position, transform.rotation);
				Destroy(gameObject);
				DestroyObject(clone, 2);
			}
		}
		else
		{
			if (playerScript.currentHealth > 1f)
			{

				playerScript.TakeDamage(damagePerShot);
			}

			if (myScript.currentHealth > 1f)
			{
				myScript.TakeDamage(damagePerShot, GetComponent<Transform>().position);
			}

			/*playerInRange = false;*/

			if (playerScript.currentHealth <= 1f)
			{

				GameObject clone;
				clone = Instantiate(playerExplosion, transform.position, transform.rotation);
				Destroy(player);
				DestroyObject(clone, 2);
				playerInRange = false;
			}

			if (myScript.currentHealth <= 1f)
			{

				GameObject clone;
				clone = Instantiate(playerExplosion, transform.position, transform.rotation);
				Destroy(gameObject);
				DestroyObject(clone, 2);
			}
		}
	}
	

	void AttackSupport()
	{
		timer = 0f;

		// if support ship has hp and is colliding with support ship, damage it. 
		if (supportScript.currentHealth > 0f){
			//Debug.Log ("Access SupportScript");
			supportScript.TakeDamage (damagePerShot);
		}

		// if this ship has hp and is colliding with support ship, damage it.
		if(myScript.currentHealth > 0f){
			myScript.TakeDamage (damagePerShot, GetComponent<Transform> ().position);
		}


		// if player's support ship health is too low, destroy it. 
		if (supportScript.currentHealth <= 0f ) {

			GameObject clone;
			clone = Instantiate (playerExplosion, transform.position, transform.rotation);
			Destroy (support);
			DestroyObject (clone, 2);
			supportInRange = false;
		}

		// if this ship health is too low, destroy it. 
		if (myScript.currentHealth <= 1f){

			GameObject clone;
			clone = Instantiate (playerExplosion, transform.position, transform.rotation);
			Destroy (gameObject);
			DestroyObject (clone, 2);
		}
		supportInRange = false;

	}
}

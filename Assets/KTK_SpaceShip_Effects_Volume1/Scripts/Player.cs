using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject explosion;

	public float waitBulletTime = 0.1f;

	private float timer = 0.0f;

	public GameObject Set_bullet;

	public GameObject bullet_Normal1;
	public GameObject bullet_LargeBall1;
	public GameObject bullet_Laser1;

	public Transform EffectPoint_Shot;

	public bool BarrierOnOff = false; //Barrie_On=true, Barrie_Off=false

	public GameObject Eff_Barrier;

	public GameObject GameController;

	public GameObject playerController;

	public GameObject Eff_Boost;

	public GameObject Eff_Warp;

	public GameObject Eff_BigLaser;

	public bool PlayerStatas = true;

	public bool LaserStatas = false;//true = Laser_ON, false = Laser_OFF

	void Start(){
		GameController = GameObject.Find("GameController");
		Set_bullet = bullet_Normal1;
	}

	void Update(){
		timer += Time.deltaTime;
		if(Input.GetMouseButton(0) && timer > waitBulletTime){
			Instantiate (Set_bullet, EffectPoint_Shot.position,transform.rotation);
			timer = 0.0f;
		}
	}

	//ChangeBullet
	void ChangeBullet(){
		if(Set_bullet == bullet_Normal1){
			Set_bullet = bullet_LargeBall1;
		}else if(Set_bullet == bullet_LargeBall1){
			Set_bullet = bullet_Laser1;
		}else {
			Set_bullet = bullet_Normal1;
		}
	}

	//Barrier_On
	void BarrierChangeOn(){
		if(Eff_Barrier != null){
			if(BarrierOnOff == false){
				BarrierOnOff = true;
				Eff_Barrier.active = true;
			}
		}
	}	

	//Barrier_Off
	void BarrierChangeOff(){
		if(Eff_Barrier != null){
			if(BarrierOnOff == true){
				BarrierOnOff = false;
				Eff_Barrier.active = false;
			}
		}
	}

	//WarpOn
	void WarpOn(){
		if(Eff_Warp != null){
			GameObject obj2 = (GameObject)Instantiate (Eff_Warp, transform.position,transform.rotation);
			obj2.transform.parent = playerController.transform;
			playerController.GetComponent<Animation>().Play("Warp_Player");
			playerController.GetComponent<SphereCollider>().enabled = false; 
			Invoke("ReturnPlayer", 2);
		}
	}

	//Return_Player
	void ReturnPlayer(){
		playerController.active = false;
		playerController.active = true;
		/*Invoke("ActivePlayerCollider", 3);*/
	}


	void LaserOn(){
		if(LaserStatas == false){
			if(Eff_BigLaser != null){
				playerController.GetComponent<SphereCollider>().enabled = false;		// Player_Collider_Off
				LaserStatas = true;

				if(PlayerStatas == true){
					GameObject obj3 = (GameObject)Instantiate (Eff_BigLaser, transform.position,transform.rotation);
					obj3.transform.parent = playerController.transform;


					Invoke("LaserOff", 3);
				}
			}
		}
	}

	void LaserOff(){
		LaserStatas = false;
		playerController.GetComponent<SphereCollider>().enabled = true;		// Player_Collider_On
	}

/*	void OnTriggerEnter(Collider c){
		if(c.tag=="Enemy"){

			Destroy(c.gameObject);
		}

		if(c.tag == "Enemy"){
			//BarrierOff && LaserOff
			if((BarrierOnOff == false) && (LaserStatas == false)){
				PlayerStatas = false;
				Instantiate (explosion, transform.position, transform.rotation);	//Create_Effect_Explosion
				GameController.SendMessage("RestartPlayer");	// RestartPlayer
				playerController.GetComponent<SphereCollider>().enabled = false;		// Player_Collider_Off
				playerController.active = false;	

				if(Eff_Boost != null){
					GameObject obj = (GameObject)Instantiate (Eff_Boost, transform.position,transform.rotation);
					obj.transform.parent = playerController.transform;
				}
				Invoke("ActivePlayerCollider", 2);	//second:2  Player_Collider_On

			}
		}
	}*/

	void ActivePlayerCollider(){
		playerController.GetComponent<SphereCollider>().enabled = true; 
		PlayerStatas = true;
		LaserStatas = false;
	}
}

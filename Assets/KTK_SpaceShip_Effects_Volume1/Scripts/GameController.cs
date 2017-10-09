using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject Player;

	public bool playerStatus = true;

	void Start(){
		Player = GameObject.Find("SpaceShip_controller");
	}

	void RestartPlayer(){
		Invoke("ActivePlayer", 1);

	}

	void ActivePlayer(){
		if(Player != null){
			Player.active = true;
		}
	}
}

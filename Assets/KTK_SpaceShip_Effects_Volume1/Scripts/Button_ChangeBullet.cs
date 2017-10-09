//======================================
/*
@autor ktk.kumamoto
@date 2015.4.21 create
@note Button_ChangeBullet
*/
//======================================

using UnityEngine;
using System.Collections;

public class Button_ChangeBullet : MonoBehaviour {

	public GameObject Player;
	private string set_bullet;
	private string bullet1 = ":Eff_Bullet_Normal_00";
	private string bullet2 = ":Eff_Bullet_LargBall_00";
	private string bullet3 = ":Eff_Bullet_Laser_00";

	void Start(){
		set_bullet = bullet1;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(120, 205, 300, 20), set_bullet);
		if(GUI.Button(new Rect(10, 200, 100, 30), "ChangeBullet")){
			if(Player != null){
				Player.SendMessage("ChangeBullet");
				if(set_bullet == bullet1){
					set_bullet = bullet2;
				}else if(set_bullet == bullet2){
					set_bullet = bullet3;
				}else {
					set_bullet = bullet1;
				}
			}
		}
	}
}

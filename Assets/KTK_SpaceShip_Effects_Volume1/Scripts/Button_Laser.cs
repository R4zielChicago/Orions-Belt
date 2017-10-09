//======================================
/*
@autor ktk.kumamoto
@date 2015.4.21 create
@note Button_Laser
*/
//======================================

using UnityEngine;
using System.Collections;

public class Button_Laser : MonoBehaviour {
	
	public GameObject Player;

	void OnGUI()
	{
		if(GUI.Button(new Rect(10, 160, 100, 30), "BigLaser")){
			if(Player != null){
				Player.SendMessage("LaserOn");
			}
		}
	}
}
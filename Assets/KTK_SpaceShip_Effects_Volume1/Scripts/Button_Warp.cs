//======================================
/*
@autor ktk.kumamoto
@date 2015.4.21 create
@note Button_Warp
*/
//======================================

using UnityEngine;
using System.Collections;

public class Button_Warp : MonoBehaviour {
	
	public GameObject Player;

	void OnGUI()
	{
		if(GUI.Button(new Rect(10, 120, 100, 30), "Warp")){
			if(Player != null){
				Player.SendMessage("WarpOn");
			}
		}
	}
}
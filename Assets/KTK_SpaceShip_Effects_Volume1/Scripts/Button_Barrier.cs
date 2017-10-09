//======================================
/*
@autor ktk.kumamoto
@date 2015.4.20 create
@note Button_Barrier
*/
//======================================

using UnityEngine;
using System.Collections;

public class Button_Barrier : MonoBehaviour {
	
	private bool isChecked = true;
	private string Barrier_State = "Barrier:Off";

	public GameObject Player;
	
	void OnGUI()
	{
		//GUI.Label(new Rect(10, 50, 300, 20), "SpaceKey: switching the start and pause");
		Rect rect1 = new Rect(10, 90, 400, 30);
		isChecked = GUI.Toggle(rect1, isChecked, Barrier_State);
		if(Player != null){
			if (isChecked ) {
				Player.SendMessage("BarrierChangeOn");
				Barrier_State = "Barrier:On";
			} else {
				Player.SendMessage("BarrierChangeOff");
				Barrier_State = "Barrier:Off";
			}
		}
	}
}
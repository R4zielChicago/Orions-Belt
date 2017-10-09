//======================================
/*
@autor ktk.kumamoto
@date 2015.3.21 create
@note Laser
*/
//======================================
using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
	private LineRenderer lr;



	void Awake()
	{
		lr = GetComponent<LineRenderer>();
	}
	
	void Update()
	{
		lr.SetPosition(1, new Vector3(0,0,5000));

	}
}

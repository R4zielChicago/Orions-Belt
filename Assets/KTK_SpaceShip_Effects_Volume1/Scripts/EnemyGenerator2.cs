using UnityEngine;
using System.Collections;

public class EnemyGenerator2 : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 GeneratPosition;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;



	void Start ()
	{
		StartCoroutine (SpawnWaves ());
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				GameObject obj = (GameObject)Instantiate (hazard, spawnPosition + GeneratPosition, spawnRotation);
				obj.transform.parent = transform;
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
		}
	}
	
}
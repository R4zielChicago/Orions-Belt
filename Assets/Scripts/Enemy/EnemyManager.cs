using UnityEngine;
using System.Collections;
//using UnityEngine.Experimental.UIElements.StyleEnums;

public class EnemyManager: MonoBehaviour
{
	public GameObject[] enemies;
	public Transform[] spawnPoints;
	public Vector3 GeneratPosition;
	public Vector3 spawnValues;
	public int enemyCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public bool useSpawnPoints;



	void Start ()
	{
		StartCoroutine (SpawnWaves ());
	}
	
	IEnumerator SpawnWaves ()
	{
		// WAIT BEFORE STARTING TO SPAWN ENEMIES
		yield return new WaitForSeconds (startWait);


		if (useSpawnPoints) {
			// SPAWN ENEMIES FROM SPAWN POINTS

			while (true) {
				for (int i = 0; i < enemyCount; i++) {
					// PICK AN ENEMY TO SPAWN
					GameObject enemy = enemies [Random.Range (0, enemies.Length)];

					// PICK A SPAWN POINT TO SPAWN ENEMIES FROM
					int spawnPoint = Random.Range (0, spawnPoints.Length);
					Transform spawn = spawnPoints [spawnPoint];
					// SPAWN ENEMY
					Instantiate (enemy, spawn.position + GeneratPosition, spawn.rotation);
					//Debug.Log ("ENEMY SPAWNED", gameObject);

					// WAIT BEFORE SPAWNING NEXT ENEMY FROM SPAWN POINT.
					yield return new WaitForSeconds (spawnWait);
				}
				yield return new WaitForSeconds (waveWait);
			}

		}else{
			// SPAWN ENEMIES FROM WITHIN VECTOR3 SPAWN VALUES
			while (true) {
				for (int i = 0; i < enemyCount; i++) {
					GameObject enemy = enemies [Random.Range (0, enemies.Length)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x),
					spawnValues.y,
						Random.Range (-spawnValues.z, spawnValues.z));
					Quaternion spawnRotation = Quaternion.identity;
					GameObject obj = (GameObject)Instantiate (enemy, spawnPosition + GeneratPosition, spawnRotation);
					obj.transform.parent = transform;
					yield return new WaitForSeconds (spawnWait);
				}
				yield return new WaitForSeconds (waveWait);
			}

		}

	}
	
}
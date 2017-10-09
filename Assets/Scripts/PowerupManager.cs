using UnityEngine;
using System.Collections;
//using UnityEngine.Experimental.UIElements.StyleEnums;

public class PowerupManager: MonoBehaviour
{
	public bool useSpawnPoints;
	public GameObject[] powerUps;
	public Transform[] spawnPoints;
	public Vector3 GeneratPosition;
	public Vector3 spawnValues;
	public int numOfPowerUps;
	public float spawnWait;
	public float startWait;
	public float waveWait;




	void Start ()
	{
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves ()
	{
		// WAIT BEFORE STARTING TO SPAWN POWERUPS
		yield return new WaitForSeconds (startWait);


		if (useSpawnPoints) {
			// SPAWN POWERUPS FROM SPAWN POINTS

			while (true) {
				for (int i = 0; i < numOfPowerUps; i++) {
					// PICK AN ENEMY TO SPAWN
					GameObject powerUp = powerUps [Random.Range (0, powerUps.Length)];

					// PICK A SPAWN POINT TO SPAWN ENEMIES FROM
					int spawnPoint = Random.Range (0, spawnPoints.Length);
					Transform spawn = spawnPoints [spawnPoint];
					// SPAWN PowerUp
					Instantiate (powerUp, spawn.position + GeneratPosition, spawn.rotation);


					// WAIT BEFORE SPAWNING NEXT ENEMY FROM SPAWN POINT.
					yield return new WaitForSeconds (spawnWait);
				}
				yield return new WaitForSeconds (waveWait);
			}

		}else{
			// SPAWN ENEMIES FROM WITHIN VECTOR3 SPAWN VALUES
			while (true) {
				for (int i = 0; i < numOfPowerUps; i++) {
					GameObject powerUp = powerUps[Random.Range (0, powerUps.Length)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x),
						spawnValues.y,
						Random.Range (-spawnValues.z, spawnValues.z));
					Quaternion spawnRotation = Quaternion.identity;
					GameObject obj = (GameObject)Instantiate (powerUp, spawnPosition + GeneratPosition, spawnRotation);
					//Debug.Log ("PowerUp Spawned", gameObject);
					obj.transform.parent = transform;
					yield return new WaitForSeconds (spawnWait);
				}
				yield return new WaitForSeconds (waveWait);
			}

		}

	}

}
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour{

	public GameObject enemyPrefab;
	public GameObject ESpawner;
	private int numberOfEnemies;
	private float RandX;
	private float RandY;


	public override void OnStartServer()
	{
		numberOfEnemies = 4;
		for (int i = 0; i < numberOfEnemies; i++) 
		{

			RandX = Random.Range (-8.0f, 8.0f);

			RandY = Random.Range (-8.0f, 8.0f);

			var spawnPosition = new Vector3 (ESpawner.transform.position.x + RandX, ESpawner.transform.position.y, ESpawner.transform.position.z + RandY);
			Debug.Log(ESpawner.transform.position);
			Debug.Log ("spawned");
			var spawnRotation = Quaternion.Euler (0.0f, Random.Range (0, 180), 0.0f);
		
			var enemy = (GameObject)Instantiate (enemyPrefab, spawnPosition, spawnRotation);
			NetworkServer.Spawn (enemy);
		
		}
	}
}
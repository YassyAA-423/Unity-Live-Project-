using System.Collections;
using UnityEditor;
using UnityEngine;

public class DS_Spawner : MonoBehaviour
{
	[SerializeField] private float spawnRate = 2f;
	[SerializeField] float maxEnemySpawn = 5f;
	[SerializeField] private GameObject[] enemyPrefab;

	private float currentEnemySpawnCount = 0;
	/*public Transform parent;
	public GameObject child;*/

	void Start()
	{
		StartCoroutine(EnemySpawner());
	}

	private IEnumerator EnemySpawner()
	{
		WaitForSeconds wait = new WaitForSeconds(spawnRate);

		while (currentEnemySpawnCount < maxEnemySpawn)
		{
			yield return wait;
			int rand = Random.Range(0, enemyPrefab.Length);
			GameObject enemyToSpawn = enemyPrefab[rand];

			Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
			currentEnemySpawnCount++;
			//child.transform.SetParent(parent.transform);
		}
	}
}

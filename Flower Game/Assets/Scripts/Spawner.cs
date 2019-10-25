using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	[SerializeField] public GameObject[] items;
	public float spawnXBound = 8.5f;
    public float spawnYBound = 4.6f;
	public float spawnWait;
	public int startWait;
	public float spawnMostWait;
	public float spawnLeastWait;
	public bool stop;

	int randItem;

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(SpawnerRoutine());
    }

    // Update is called once per frame
    void Update()
    {
		spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator SpawnerRoutine()
	{
		yield return new WaitForSeconds(startWait);

        while(!stop)
		{
            //TODO randomize
			randItem = Random.Range(0, items.Length);
			Vector3 spawnPosition = new Vector3(Random.Range(-spawnXBound, spawnXBound), Random.Range(-spawnYBound, spawnYBound));
			Instantiate(items[randItem], spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(spawnWait);
		}
	}
}

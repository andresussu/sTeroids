using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerShootingStar : MonoBehaviour
{

	public GameObject shootingStarPrefab;
	public float spawnTime;
	public float counter;
	private Camera cam;

	void Start()
	{
		cam = Camera.main;

		counter = spawnTime;
	}

	private void Update()
	{
		if (counter <= 0)
		{
			spawnTime = Random.Range(8, 20);
			counter = spawnTime;
			Spawn();
		}

		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			Spawn();
		}

		counter -= Time.deltaTime;
	}

	public void Spawn()
	{
		Vector2 position = new Vector2(Random.Range(-cam.orthographicSize * cam.aspect, cam.orthographicSize * cam.aspect), Random.Range(-cam.orthographicSize, cam.orthographicSize));

		Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

		GameObject newObject = Instantiate(shootingStarPrefab, position, rotation) as GameObject;
	}

}

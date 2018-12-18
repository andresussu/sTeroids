using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

	public GameObject asteroidPrefab;
	public float startCounter = 3f;
	private float counter;
	private Camera cam;

	public Image spawnBar;

	public Rigidbody2D player;

	public GameController gameController;
	public DebugController debugController;
	private float difficultyOverTime = 1f;

	public string asteroidTag = "Asteroid";

	public int numAsteroids;
	public int maxAsteroids = 80;
	private bool canSpawn;
	public float playerSpawnThreshold = 1f;
	public float asteroidSpawnThreshold = .5f;

	private float maxSpawnDistance = 0.1f;

	public GameObject marker;

	void Start()
	{
		counter = startCounter;

		cam = Camera.main;
	}

	private void Update()
	{
		CountAsteroids();

		if (counter < 0 && numAsteroids < maxAsteroids)
		{
			Vector2 position = new Vector2(Random.Range(-cam.orthographicSize * cam.aspect, cam.orthographicSize * cam.aspect), Random.Range(-cam.orthographicSize, cam.orthographicSize));

			SpawnCheck(position);

			if (canSpawn)
			{
				counter = startCounter;

				if (Random.Range(0f, 1f) < .3f)
				{
					Spawn(asteroidPrefab, position, 1);
				}
				else
				{
					Spawn(asteroidPrefab, position, 2);
				}
			}
		}

		difficultyOverTime += Time.deltaTime;

		counter -= (Time.deltaTime * gameController.difficulty) + (difficultyOverTime * .0001f);

		spawnBar.fillAmount = counter / startCounter;
	}

	public void Spawn(GameObject _prefab,Vector2 _position, int _health)
	{
		Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

		GameObject newObject = Instantiate(_prefab, _position, rotation) as GameObject;

		newObject.GetComponent<Asteroids>().health = _health;
	}

	
	public void SpawnOnDeath(Vector2 _position)
	{
		Vector2 position01 = new Vector2(_position.x + (_position.x * Random.Range(-maxSpawnDistance, maxSpawnDistance)), _position.y + (_position.y * Random.Range(-maxSpawnDistance, maxSpawnDistance)));
		Quaternion rotation01 = Quaternion.Euler(0, 0, Random.Range(0, 180));

		GameObject newObject01 = Instantiate(asteroidPrefab, position01, rotation01) as GameObject;

		newObject01.GetComponent<Asteroids>().health = 1;

		Vector2 position02 = new Vector2(_position.x + (_position.x * Random.Range(-maxSpawnDistance, maxSpawnDistance)), _position.y + (_position.y * Random.Range(-maxSpawnDistance, maxSpawnDistance)));
		Quaternion rotation02 = Quaternion.Euler(0, 0, Random.Range(180, 360));

		GameObject newObject02 = Instantiate(asteroidPrefab, position02, rotation02) as GameObject;

		newObject02.GetComponent<Asteroids>().health = 1;
	}
	

	public void SpawnCheck(Vector2 _position)
	{
		// Player position check

		if ((_position.x < player.position.x + playerSpawnThreshold && _position.x > player.position.x - playerSpawnThreshold) && (_position.y < player.position.y + playerSpawnThreshold && _position.y > player.position.y - playerSpawnThreshold))
		{
			canSpawn = false;

			if (debugController.isDebugging)
			{
				Debug.Log("Spawner script: Invalid position to spawn. Player too close.");

				GameObject newObject = Instantiate(marker, _position, Quaternion.identity) as GameObject;
				Destroy(newObject, 5f);
			}
		}
		else
		{
			canSpawn = true;
		}

		// Asteroids position check

		if (canSpawn)
		{
			GameObject[] asteroids = GameObject.FindGameObjectsWithTag(asteroidTag);
			foreach (GameObject asteroid in asteroids)
			{
				Vector2 asteroidPos = asteroid.transform.position;

				if ((_position.x < asteroidPos.x + asteroidSpawnThreshold && _position.x > asteroidPos.x - asteroidSpawnThreshold) && (_position.y < asteroidPos.y + asteroidSpawnThreshold && _position.y > asteroidPos.y - asteroidSpawnThreshold))
				{
					canSpawn = false;

					if (debugController.isDebugging)
					{
						Debug.Log("Spawner script: Invalid position to spawn. Asteroid too close");

						GameObject newObject = Instantiate(marker, _position, Quaternion.identity) as GameObject;
						Destroy(newObject, 5f);
					}

					return;
				}
				else
				{
					canSpawn = true;
				}
			}
		}
	}

	private void CountAsteroids()
	{
		numAsteroids = 0;

		GameObject[] asteroids = GameObject.FindGameObjectsWithTag(asteroidTag);

		foreach (GameObject asteroid in asteroids)
		{
			numAsteroids += 1;
		}
	}

}

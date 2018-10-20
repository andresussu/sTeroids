using UnityEngine;
using UnityEngine.UI;

public class SpawnerMenu : MonoBehaviour
{

	public GameObject asteroidPrefab;
	public float startCounter = 5f;
	private float counter;
	private Camera cam;

	public float difficultyOverTime = 1f;

	public string logoTag = "Logo";

	public string asteroidTag = "Asteroid";

	private bool canSpawn = true;
	public float logoSpawnThreshold = .5f;
	public float asteroidSpawnThreshold = .5f;

	public int maxAsteroids = 20;
	public int numAsteroids = 0;

	public bool markerOn = false;
	public GameObject marker;

	void Start()
	{
		counter = startCounter;

		cam = Camera.main;
	}

	private void Update()
	{

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

		if(Input.GetKeyDown(KeyCode.P))
		{
			DestroyAll();
		}

		difficultyOverTime += 20 * Time.deltaTime;

		counter -= (Time.deltaTime * 5) + (difficultyOverTime * .0001f);
	}

	public void Spawn(GameObject _prefab,Vector2 _position, int _health)
	{
		CountAsteroids();

		Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

		if (markerOn)
		{
			Debug.Log(_position);

			GameObject gameObject = Instantiate(marker, _position, Quaternion.identity) as GameObject;
			Destroy(gameObject, 5f);
		}

		GameObject newObject = Instantiate(_prefab, _position, rotation) as GameObject;

		newObject.GetComponent<AsteroidsMenu>().health = _health;

	}

	public void SpawnCheck(Vector2 _position)
	{
		// Logo position check

		GameObject[] logos = GameObject.FindGameObjectsWithTag(logoTag);
		foreach (GameObject logo in logos)
		{
			Vector2 logoPos = logo.transform.position;

			if ((_position.x < logoPos.x + logoSpawnThreshold && _position.x > logoPos.x - logoSpawnThreshold) && (_position.y < logoPos.y + logoSpawnThreshold && _position.y > logoPos.y - logoSpawnThreshold))
			{
				canSpawn = false;

				if (markerOn)
				{
					Debug.Log("Invalid position to spawn. Logo too close");

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

					if (markerOn)
					{
						Debug.Log("Invalid position to spawn. Asteroid too close");

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

	public void DestroyAll()
	{
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag(asteroidTag);

		foreach (GameObject asteroid in asteroids)
		{
			Destroy(asteroid);
			numAsteroids = 0;
		}
	}

}

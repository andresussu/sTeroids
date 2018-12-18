using UnityEngine;
using UnityEngine.UI;

public class Asteroids : MonoBehaviour
{
	public GameObject asteroid;
	public Rigidbody2D asteroidRB;

	public Sprite[] sprites;
	public string resourceName;

	public float minSpeed;
	public float maxSpeed;

	[HideInInspector]
	public int health;

	private Vector2 scale;

	public int worth = 50;

	public GameObject deathEffect;
	public GameObject audioController;

	public GameObject player;

	public GameObject spawner;


	private Camera cam;

	public float angularVelocity;
	public float angularVelocityThreshold = 100f;

	void Start()
	{
		if (resourceName != "")
		{
			sprites = Resources.LoadAll<Sprite>(resourceName);

			GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
		}

		asteroid.AddComponent(typeof(PolygonCollider2D));

		cam = Camera.main;

		transform.localScale = new Vector2(0, 0);

		if (health == 1)
		{
			scale = new Vector2(.6f, .6f);
		}
		else
		{
			scale = new Vector2(1f, 1f);
		}

		GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(minSpeed, maxSpeed));
		GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5f, 5f));

	}

	private void Update()
	{
		angularVelocity = asteroidRB.angularVelocity;

		transform.localScale = Vector2.Lerp(transform.localScale, scale, 10f * Time.deltaTime);

		if (asteroidRB.angularVelocity > angularVelocityThreshold)
		{
			asteroidRB.angularDrag = .5f * asteroidRB.angularVelocity;
		}
		else if(asteroidRB.angularVelocity < -angularVelocityThreshold)
		{
			asteroidRB.angularDrag = -.5f * asteroidRB.angularVelocity;
		}
		else
		{
			asteroidRB.angularDrag = 0;
		}
	}

	private void FixedUpdate()
	{
		Vector3 asteroidPos = this.transform.position;

		if (asteroidPos.x > (cam.orthographicSize * cam.aspect) || asteroidPos.x < (-cam.orthographicSize * cam.aspect))
		{
			asteroidPos.x *= -0.99f;
		}

		if (asteroidPos.y > cam.orthographicSize || asteroidPos.y < -cam.orthographicSize)
		{
			asteroidPos.y *= -0.99f;
		}

		this.transform.position = asteroidPos;
	}

	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			Destroy(gameObject);
		}
		else
		{
			Die();
		}
	}	

	public void Die()
	{
		Player p = player.GetComponent<Player>();

		Spawner s = spawner.GetComponent<Spawner>();

		AudioController audioC = audioController.GetComponent<AudioController>();

		if (health == 2)
		{
			Player.Score += worth;
			Vector2 position = transform.position;
			
			s.SpawnOnDeath(position);

			p.ScoreReward();
			audioC.PlayAudio("Big asteroid death sound");
		}
		
		if(health == 1)
		{
			Player.Score += worth;

			p.ScoreReward();
			audioC.PlayAudio("Small asteroid death sound");
		}

		

		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
		Destroy(effect, 5f);

		Destroy(gameObject);
		
	}

	/*
	public void Spawn(Vector2 _position, int _health)
	{
		Vector2 position01 = new Vector2(_position.x + (_position.x * Random.Range(-maxSpawnDistance, maxSpawnDistance)), _position.y + (_position.y * Random.Range(-maxSpawnDistance, maxSpawnDistance)));
		Quaternion rotation01 = Quaternion.Euler(0, 0, Random.Range(0, 180));

		GameObject newObject01 = Instantiate(asteroid, position01, rotation01) as GameObject;

		newObject01.GetComponent<Asteroids>().health = _health;

		Vector2 position02 = new Vector2(_position.x + (_position.x * Random.Range(-maxSpawnDistance, maxSpawnDistance)), _position.y + (_position.y * Random.Range(-maxSpawnDistance, maxSpawnDistance)));
		Quaternion rotation02 = Quaternion.Euler(0, 0, Random.Range(180, 360));

		GameObject newObject02 = Instantiate(asteroid, position02, rotation02) as GameObject;

		newObject02.GetComponent<Asteroids>().health = _health;
	}
	*/
}
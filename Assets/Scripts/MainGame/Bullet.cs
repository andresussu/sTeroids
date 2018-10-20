using UnityEngine;

public class Bullet : MonoBehaviour
{
	private GameObject targetMarker;

	private Transform target;
	private Asteroids targetAsteroid;

	public float speed = 5f;
	public float speedRotation = 5f;
	public float lifeTime;
	public bool isHomming = false;

	public string asteroidTag = "Asteroid";

	private Rigidbody2D rb;

	private Camera cam;

	//Audio

	[Range(-1f, 0f)]
	public float engineOffset;

	public float gain;
	public float volume = .4f;

	private GameObject audioController;

	System.Random rand = new System.Random();


	private void Start()
	{
		cam = Camera.main;

		rb = GetComponent<Rigidbody2D>();

		Physics2D.IgnoreLayerCollision(8, 10);

		if (isHomming)
		{
			speed = 5f;
			lifeTime = 3f;
			InvokeRepeating("UpdateTarget", 0f, 0.5f);
		}
		else
		{
			speed = 8f;
			lifeTime = 1f;
		}

		//Audio

		audioController = GameObject.FindGameObjectWithTag("AudioController");

		AudioController audioC = audioController.GetComponent<AudioController>();
		gain = volume * audioC.mainVolume;

	}

	void UpdateTarget()
	{
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag(asteroidTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestAsteroid = null;
		foreach (GameObject asteroid in asteroids)
		{
			float distanceToAsteroid = Vector2.Distance(rb.position, asteroid.transform.position);
			if (distanceToAsteroid < shortestDistance)
			{
				shortestDistance = distanceToAsteroid;
				nearestAsteroid = asteroid;
			}
		}

		if (nearestAsteroid != null)
		{
			target = nearestAsteroid.transform;
			targetAsteroid = nearestAsteroid.GetComponent<Asteroids>();
		}
		else
		{
			target = null;
		}
	}

	void FixedUpdate()
	{
		EdgeUpdate();

		if (lifeTime > 0f && !GameController.GameIsOver)
		{

			if (isHomming == true)
			{

				if (target != null)
				{
					Vector2 dir = (Vector2)target.position - rb.position;

					dir.Normalize();

					float rotateAmount = Vector3.Cross(dir, transform.up).z;

					rb.angularVelocity = -rotateAmount * speedRotation;

					rb.velocity = transform.up * speed;
				}
				else
				{
					rb.velocity = transform.up * speed;
				}
								
			}
			else
			{
				rb.velocity = transform.up * speed;
			}
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		lifeTime -= Time.deltaTime;

	}

	private void EdgeUpdate()
	{
		Vector3 bulletPos = this.transform.position;

		if (bulletPos.x > (cam.orthographicSize * cam.aspect) || bulletPos.x < (-cam.orthographicSize * cam.aspect))
		{
			bulletPos.x *= -0.99f;
		}

		if (bulletPos.y > cam.orthographicSize || bulletPos.y < -cam.orthographicSize)
		{
			bulletPos.y *= -0.99f;
		}

		this.transform.position = bulletPos;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(gameObject);
	}

	//Audio

	void OnAudioFilterRead(float[] data, int channels)
	{
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = gain * (float)(rand.NextDouble() * 2.0 - 1.0 + engineOffset);
		}
	}

}
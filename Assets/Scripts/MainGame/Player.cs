using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Player : MonoBehaviour
{
	public GameObject player;
	public Rigidbody2D playerRigidBody;
	public SpriteRenderer playerSpriteR;

	public ParticleSystem rocketFlames;

	public static int Score;
	public int startScore = 0;

	public static int Lives;
	public int startLives = 3;


	public static int Missiles = 3;
	public int startMissiles = 3;


	[Header("Player Config")]

	public int maxLives = 20;

	public int maxMissiles = 10;

	public int scoreReward = 1000;

	public float movementSpeed = 5.0f;
	public float rotationSpeed = 360.0f;

	public float fireRate = 1f;
	private float fireCountdown;
	public Image fireBar;

	private bool canMove = true;

	private bool isFire = false;
	private bool isMissil = false;

	public bool isBoosting = false;
	private int rotation = 0;

	public GameObject deathEffect;
	public GameObject audioController;

	public GameObject standardBulletPrefab;
	public GameObject missileBulletPrefab;
	public GameObject audioFirePrefab;
	public Transform firePoint;

	private Camera cam;

	//Audio
	public float gain;
	public float volume = 1f;

	[Range(-1f, 1f)]
	public float engineOffset;

	System.Random rand = new System.Random();

	void Start()
	{
		cam = Camera.main;

		Missiles = startMissiles;

		Score = startScore;
		Lives = startLives;

		playerRigidBody = GetComponent<Rigidbody2D>();

	}

	void Update()
	{
		AudioController audioC = audioController.GetComponent<AudioController>();

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			Boost();
		}
		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
		{
			Boost();
		}

		if (isBoosting && canMove)
		{
			playerRigidBody.drag = movementSpeed * .33f;
			GetComponent<Rigidbody2D>().AddForce(transform.up * movementSpeed);
			rocketFlames.Play();
		}
		else
		{
			playerRigidBody.drag = 0f;
			rocketFlames.Stop();
		}

		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			RotateLeft();
		}

		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			RotateRight();
		}

		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			RotateLeft();
		}

		if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
		{
			RotateRight();
		}

		if (rotation != 0)
		{
			transform.Rotate(0, 0, Time.deltaTime * rotationSpeed * rotation);
		}


		if (Input.GetKeyUp(KeyCode.Space))
		{
			BulletStandard();
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			BulletStandard();
		}
		
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			BulletMissile();
		}
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			BulletMissile();
		}
		
		if (isFire && fireCountdown == 0)
		{
			fireCountdown = 1f / fireRate;

			audioC.PlayAudio("Stadard shot sound");

			GameObject bullet = (GameObject)Instantiate(standardBulletPrefab, firePoint.position, firePoint.rotation);
			bullet.GetComponent<Bullet>().isHomming = false;

			GameObject audioFire = (GameObject)Instantiate(audioFirePrefab, firePoint.position, firePoint.rotation);
			Destroy(audioFire, 2f);
		}

		if (isMissil && fireCountdown == 0 && Missiles > 0)
		{
			fireCountdown = 1f / fireRate;

			Missiles -= 1;
			audioC.PlayAudio("Missile shot sound");

			GameObject bullet = (GameObject)Instantiate(missileBulletPrefab, firePoint.position, firePoint.rotation);
			bullet.GetComponent<Bullet>().isHomming = true;
		}

		fireCountdown -= Time.deltaTime;
		if (fireCountdown <= 0f)
		{
			fireCountdown = 0f;
		}

		fireBar.fillAmount = 1f - (fireCountdown / fireRate);

		//Audio
		if (isBoosting)
		{
			gain = volume * audioC.mainVolume;
		}
		else
		{
			gain = 0;
		}

	}

	private void FixedUpdate()
	{
		
		Vector3 playerPos = player.transform.position;

		if (playerPos.x > (cam.orthographicSize * cam.aspect) || playerPos.x < (-cam.orthographicSize * cam.aspect))
		{
			playerPos.x *= -0.99f;			
		}

		if (playerPos.y > cam.orthographicSize || playerPos.y < -cam.orthographicSize)
		{
			playerPos.y *= -0.99f;
		}

		player.transform.position = playerPos;
		
	}

	
	public void BulletStandard()
	{
		isFire = !isFire;
	}

	public void BulletMissile()
	{
		isMissil = !isMissil;
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		Die();
	}

	public void Boost()
	{
		isBoosting = !isBoosting;
	}

	public void RotateLeft()
	{
		if (rotation == 0)
		{
			rotation = 1;
		}
		else
		{
			rotation = 0;
		}
	}

	public void RotateRight()
	{
		if (rotation == 0)
		{
			rotation = -1;
		}
		else
		{
			rotation = 0;
		}
	}

	void Die()
	{
		Lives -= 1;

		AudioController audioC = audioController.GetComponent<AudioController>();

		audioC.PlayAudio("Player death sound");

		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);

		if (Lives > 0)
		{
			StartCoroutine(Wait());
		}
		else
		{
			player.SetActive(false);
		}

	}

	void ResetPlayer()
	{
		//player.transform.position = new Vector3(0, 0, 0); // Reset player position on death to 0,0,0
		playerRigidBody.velocity = Vector3.zero;
	}

	IEnumerator Wait()
	{
		Color alpha = playerSpriteR.color;
		alpha.a = 0f;
		playerSpriteR.color = alpha;

		canMove = false;

		ResetPlayer();

		gain = 0;

		yield return new WaitForSecondsRealtime(.5f);
		alpha.a = 255f;
		playerSpriteR.color = alpha;

		canMove = true;
	}

	public void ScoreReward()
	{
		if ((Score % scoreReward) == 0 && Missiles <= 10)
		{
			Missiles += 1;
		}
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

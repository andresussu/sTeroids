using UnityEngine;
using UnityEngine.UI;

public class AsteroidsMenu : MonoBehaviour
{
	public GameObject asteroid;
	public Rigidbody2D asteroidRB2D;

	public Sprite[] sprites;
	public string resourceName;
	
	public float minSpeed;
	public float maxSpeed;

	public int health;

	private Vector2 scale;

	public int worth = 50;

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

		PolygonCollider2D polygonCollider2D = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;

		GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(minSpeed, maxSpeed));
		GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5f, 5f));

		
	}

	private void Update()
	{
		angularVelocity = asteroidRB2D.angularVelocity;

		transform.localScale = Vector2.Lerp(transform.localScale, scale, 10f * Time.deltaTime);

		if (asteroidRB2D.angularVelocity > angularVelocityThreshold)
		{
			asteroidRB2D.angularDrag = .5f * asteroidRB2D.angularVelocity;
		}
		else if(asteroidRB2D.angularVelocity < -angularVelocityThreshold)
		{
			asteroidRB2D.angularDrag = -.5f * asteroidRB2D.angularVelocity;
		}
		else
		{
			asteroidRB2D.angularDrag = 0;
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

}
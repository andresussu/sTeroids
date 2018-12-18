using UnityEngine;

public class ShootingStar : MonoBehaviour
{
	public GameObject shootingStar;
	public Rigidbody2D shootingStarRB;

	public TrailRenderer trailRenderer;

	public float minSpeed;
	public float maxSpeed;

	private Vector2 scale;

	public float life;
	private float trailLife;

	public GameObject audioController;

	void Start()
	{
		shootingStarRB = shootingStar.GetComponent<Rigidbody2D>();
		trailRenderer = shootingStar.GetComponent<TrailRenderer>();

		life = Random.Range(1,3);
		trailLife = life / 2;

		minSpeed = 1;
		maxSpeed = 5;

		trailRenderer.time = trailLife;

		/*
		Gradient color = new Gradient();

		color.colorKeys.SetValue()
		color.r = Random.Range(30, 255);
		color.g = color.r;
		color.b = color.r;
		color.a = 255;
		//trailRenderer.material.SetColor("Trail", color);

		trailRenderer.colorGradient = color;
		*/
		
	}

	private void Update()
	{
		life -= Time.deltaTime;

		if (life < 0)
		{
			Destroy(this.gameObject);
		}

		if (life < trailLife)
		{
			shootingStarRB.velocity = Vector2.zero;
		}
		else
		{
			GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(minSpeed, maxSpeed));
		}


	}
}
using UnityEngine;

public class ShootingStar : MonoBehaviour
{
	public GameObject shootingStar;
	public Rigidbody2D shootingStarRB;

	public TrailRenderer trailRenderer;
	public Material trailMaterial;

	public float minSpeed;
	public float maxSpeed;

	private Vector2 scale;

	public float life;
	private float trailLife;

	public GameObject audioController;
	private float randonNum;

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
		randonNum = Random.Range(20, 128);

		Color color = new Color();
		color.r = Mathf.RoundToInt(randonNum);
		color.g = color.r;
		color.b = color.r;
		color.a = 128;
		//trailMaterial.color = color;
		//trailRenderer.material.SetColor("Trail", color);
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
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
	public GameObject pDamage;

	public SpriteRenderer sp;

	public AnimationCurve curve;


	// Use this for initialization
	void Start()
	{

		sp = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{

		float t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime;
			float a = curve.Evaluate(t);
			sp.color = new Color(255f, 0f, 0f, a);
			return;
		}
	}
}

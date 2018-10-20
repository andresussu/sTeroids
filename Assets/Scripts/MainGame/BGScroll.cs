using UnityEngine;

public class BGScroll : MonoBehaviour
{
	public GameObject bgStars;

	public Rigidbody2D player;

	public Material bgStars01;
	public Material bgStars02;
	public Material bgStars03;

	float paralax = 5f;

	private Vector2 startOffset01;

	private Camera cam;

	public PauseMenu pauseMenu;

	private void Start()
	{
		startOffset01 = bgStars01.mainTextureOffset;
		cam = Camera.main;

	}

	void Update()
	{
		SetBGSize();

		if (!GameController.GameIsPaused)
		{
			MeshRenderer mr = GetComponent<MeshRenderer>();

			bgStars01 = mr.material;

			Vector2 offset = bgStars01.mainTextureOffset;

			offset.x += (player.velocity.x / 100f) / transform.localScale.x / paralax;
			offset.y += (player.velocity.y / 100f) / transform.localScale.y / paralax;

			bgStars01.mainTextureOffset = offset;
			bgStars02.mainTextureOffset = offset / 2f;
			bgStars03.mainTextureOffset = offset / 3f;
		}
	}

	public void SetBGSize()
	{
		if(cam.scaledPixelWidth < cam.scaledPixelHeight)
		{
			bgStars.transform.localScale = new Vector3(cam.orthographicSize * 2.01f, cam.orthographicSize * 2.01f, 1f);
		}
		else
		{
			bgStars.transform.localScale = new Vector3(cam.orthographicSize * cam.aspect * 2.01f, cam.orthographicSize * cam.aspect * 2.01f, 1f);
		}
	}
}

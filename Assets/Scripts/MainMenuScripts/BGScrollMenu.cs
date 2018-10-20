using UnityEngine;

public class BGScrollMenu : MonoBehaviour
{
	public GameObject bgStars;

	public Material bgStars01;
	public Material bgStars02;
	public Material bgStars03;

	float paralax = 5f;

	private Vector2 startOffset01;

	private Camera cam;


	private void Start()
	{
		startOffset01 = bgStars01.mainTextureOffset;
		cam = Camera.main;

	}

	void Update()
	{
		SetBGSize();

		MeshRenderer mr = GetComponent<MeshRenderer>();

		bgStars01 = mr.material;

		Vector2 offset = bgStars01.mainTextureOffset;

		offset.x += .001f / transform.localScale.x / paralax;

		bgStars01.mainTextureOffset = offset;
		bgStars02.mainTextureOffset = offset / 2f;
		bgStars03.mainTextureOffset = offset / 3f;

	}

	public void SetBGSize()
	{
		if(cam.scaledPixelWidth < cam.scaledPixelHeight)
		{

			bgStars.transform.localScale = new Vector3(cam.orthographicSize, cam.orthographicSize, 1f);
		}
		else
		{
			bgStars.transform.localScale = new Vector3(cam.orthographicSize * cam.aspect, cam.orthographicSize * cam.aspect, 1f);
		}

	}
}

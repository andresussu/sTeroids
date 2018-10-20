using UnityEngine;

public class Tutorial : MonoBehaviour
{
	public float tutorialTimer = 5f;

	private CanvasGroup tutorialCanvas;

	public float alpha = 1f;

	public float fadeOutTime;
	public float fadeOutCount;

	void Start ()
	{
		tutorialCanvas = this.GetComponent<CanvasGroup>();
		tutorialCanvas.alpha = alpha;
	}

	void Update ()
	{
		if (tutorialTimer > 0)
		{
			TutorialTimer();
			fadeOutCount = 1f / fadeOutTime;
		}

		fadeOutCount -= Time.deltaTime;
		if (fadeOutCount <= 0f)
		{
			PlayerPrefs.SetInt("Tutorial", 0);
			Destroy(this.gameObject);
		}

		alpha = fadeOutCount;

		tutorialCanvas.alpha = alpha;
	}

	void TutorialTimer()
	{
		tutorialTimer -= Time.deltaTime;
		if (tutorialTimer < 0)
		{
			tutorialTimer = 0;
		}
	}

}

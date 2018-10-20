using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
	public Text livesText;

	public Image[] livesImages = new Image[20];

	public Text missilesText;

	public Image[] missilesImages = new Image[10];

	public Text time;
	private float counterSec = 0f;
	private float counterMin = 0f;
	private float counterHour = 0f;

	public Text scoreText;

	void FixedUpdate()
	{

		if (Player.Lives == 1)
		{
			livesText.text = Player.Lives.ToString() + " Live";
		}
		else
		{
			livesText.text = Player.Lives.ToString() + " Lives";
		}

		LivesImage();

		missilesText.text = Player.Missiles.ToString() + " Missiles";

		MissilesImage();

		Timer();

		scoreText.text = "Score " + Player.Score.ToString();



	}

	void Timer()
	{
		string timeText = "";

		counterSec += Time.deltaTime;

		if (counterSec >= 60f)
		{
			counterSec = 0f;
			counterMin += 1f;
		}

		if (counterMin >= 60f)
		{
			counterMin = 0f;
			counterHour += 1f;
		}

		if (counterHour < 10)
		{
			timeText += "0" + counterHour;
		}
		else
		{
			timeText += counterHour;
		}
		timeText += ":";

		if (counterMin < 10)
		{
			timeText += "0" + counterMin;
		}
		else
		{
			timeText += counterMin;
		}
		timeText += ":";

		if (counterSec < 9.5)
		{
			timeText += "0" + Mathf.RoundToInt(counterSec);
		}
		else
		{
			timeText += Mathf.RoundToInt(counterSec);
		}

		time.text = timeText;
	}

	void LivesImage()
	{
		Color color;

		for (int i = 0; i < livesImages.Length; i++)
		{
			if (i < Player.Lives)
			{
				color = new Color(255, 255, 255, 255);
				livesImages[i].color = color;
			}
			else
			{
				color = new Color(255, 255, 255, 0);
				livesImages[i].color = color;
			}

		}
	}

	void MissilesImage()
	{
		Color color;

		for (int i = 0; i < missilesImages.Length; i++)
		{
			if (i < Player.Missiles)
			{
				color = new Color(255, 255, 255, 255);
				missilesImages[i].color = color;
			}
			else
			{
				color = new Color(255, 255, 255, 0);
				missilesImages[i].color = color;
			}

		}
	}
}

